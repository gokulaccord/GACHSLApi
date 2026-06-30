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

        public async Task<string> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return "Email already exists.";
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

            return "User registered successfully.";
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

            var loginResponse = new LoginResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };

            return new ApiResponse<LoginResponseDto>(
                true,
                "Login successful.",
                loginResponse);
        }
    }
}