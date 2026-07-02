using BCrypt.Net;
using GACHSLApi.Common;
using GACHSLApi.DTOs;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using GACHSLApi.Helpers;

namespace GACHSLApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordResetOtpRepository _otpRepository;
        private readonly IEmailService _emailService;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        //public AuthService(
        //    IUserRepository userRepository,
        //    IJwtService jwtService)
        //{
        //    _userRepository = userRepository;
        //    _jwtService = jwtService;
        //}

        public async Task<ApiResponse<object>> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return new ApiResponse<object>(
                    false,
                    "Email already exists.",
                    null);
            }

            var user = new User
            {
                Username = request.Username,
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User",   // FORCE DEFAULT
                IsActive = true,
                CreatedOn = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "User registered successfully.",
                null);
        }
        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return new ApiResponse<LoginResponseDto>(
                    false,
                    "Invalid email or password.",
                    null);
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!isPasswordValid)
            {
                return new ApiResponse<LoginResponseDto>(
                    false,
                    "Invalid email or password.",
                    null);
            }

            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                CreatedOn = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            var loginResponse = new LoginResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = token,
                RefreshToken = refreshToken
            };

            return new ApiResponse<LoginResponseDto>(
                true,
                "Login successful.",
                loginResponse);
        }

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordResetOtpRepository otpRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _otpRepository = otpRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _emailService = emailService;
        }
        public async Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var storedToken = await _refreshTokenRepository
                .GetValidRefreshTokenAsync(request.RefreshToken);

            if (storedToken == null)
            {
                return new ApiResponse<LoginResponseDto>(
                    false,
                    "Invalid or expired refresh token.",
                    null);
            }

            // Revoke the old refresh token
            storedToken.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(storedToken);

            // Generate new tokens
            var newAccessToken = _jwtService.GenerateToken(storedToken.User);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Save new refresh token
            var refreshTokenEntity = new RefreshToken
            {
                UserId = storedToken.UserId,
                Token = newRefreshToken,
                CreatedOn = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            var response = new LoginResponseDto
            {
                UserId = storedToken.User.UserId,
                Username = storedToken.User.Username,
                FullName = storedToken.User.FullName,
                Email = storedToken.User.Email,
                Role = storedToken.User.Role,
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return new ApiResponse<LoginResponseDto>(
                true,
                "Token refreshed successfully.",
                response);
        }
        public async Task<ApiResponse<object>> LogoutAsync(RefreshTokenRequestDto request)
        {
            var storedToken = await _refreshTokenRepository
                .GetValidRefreshTokenAsync(request.RefreshToken);

            if (storedToken == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Invalid or expired refresh token.",
                    null);
            }

            // Revoke the refresh token
            storedToken.IsRevoked = true;

            await _refreshTokenRepository.UpdateAsync(storedToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Logout successful.",
                null);
        }
        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public async Task<ApiResponse<object>> ForgotPasswordAsync(ForgotPasswordDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return new ApiResponse<object>(false, "User not found.", null);

            var otp = GenerateOtp();

            var otpEntity = new PasswordResetOtp
            {
                UserId = user.UserId,
                Otp = otp,
                CreatedOn = DateTime.UtcNow,
                ExpiryOn = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };

            await _otpRepository.AddAsync(otpEntity);
            await _otpRepository.SaveChangesAsync();

            var subject = "GACHSL Password Reset OTP";

            var body = EmailTemplate.GetOtpTemplate(
                user.FullName,
                otp);

            await _emailService.SendEmailAsync(
                user.Email,
                subject,
                body);

            return new ApiResponse<object>(
                true,
                "OTP has been sent to your registered email address.",
                null);
        }
        public async Task<ApiResponse<object>> VerifyOtpAsync(VerifyOtpDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return new ApiResponse<object>(false, "User not found.", null);

            var otpRecord = await _otpRepository.GetValidOtpAsync(user.UserId, request.Otp);

            if (otpRecord == null)
                return new ApiResponse<object>(false, "Invalid or expired OTP.", null);

            otpRecord.IsUsed = true;
            await _otpRepository.SaveChangesAsync();

            // Generate secure reset token
            var resetToken = Guid.NewGuid().ToString();

            // Save token
            var passwordResetToken = new PasswordResetToken
            {
                UserId = user.UserId,
                Token = resetToken,
                CreatedOn = DateTime.UtcNow,
                ExpiryOn = DateTime.UtcNow.AddMinutes(15),
                IsUsed = false
            };

            await _passwordResetTokenRepository.AddAsync(passwordResetToken);
            await _passwordResetTokenRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "OTP verified successfully.",
                new
                {
                    ResetToken = resetToken
                });
        }
        public async Task<ApiResponse<object>> ResetPasswordAsync(ResetPasswordDto request)
        {
            var token = await _passwordResetTokenRepository
                .GetValidTokenAsync(request.ResetToken);

            if (token == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Invalid or expired reset token.",
                    null);
            }

            token.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            token.IsUsed = true;

            await _userRepository.UpdateAsync(token.User);
            await _passwordResetTokenRepository.UpdateAsync(token);

            await _userRepository.SaveChangesAsync();
            await _passwordResetTokenRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Password reset successfully.",
                null);
        }
    }
}