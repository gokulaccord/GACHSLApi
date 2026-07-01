using BCrypt.Net;
using GACHSLApi.Common;
using GACHSLApi.DTOs;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

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
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
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
    }
}