using GACHSLApi.Common;
using GACHSLApi.DTOs;
using GACHSLApi.Interfaces;
using BCrypt.Net;

namespace GACHSLApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserResponseDto>> GetCurrentUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return new ApiResponse<UserResponseDto>(
                    false,
                    "User not found.",
                    null);
            }

            var response = new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedOn = user.CreatedOn
            };

            return new ApiResponse<UserResponseDto>(
                true,
                "User fetched successfully.",
                response);
        }

        public async Task<ApiResponse<List<UserResponseDto>>> GetAllUsersAsync(UserQueryDto query)
        {
            var users = await _userRepository.GetAllAsync();

            // 🔍 SEARCH
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                users = users.Where(u =>
                    u.Username.Contains(query.Search) ||
                    u.Email.Contains(query.Search) ||
                    u.FullName.Contains(query.Search)
                ).ToList();
            }

            // 📊 TOTAL BEFORE PAGINATION
            var totalRecords = users.Count;

            // 📄 PAGINATION
            var pagedUsers = users
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var result = pagedUsers.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedOn = user.CreatedOn
            }).ToList();

            return new ApiResponse<List<UserResponseDto>>(
                true,
                $"Users fetched successfully. Total: {totalRecords}",
                result
            );
        }
        public async Task<ApiResponse<object>> ChangePasswordAsync(int userId, ChangePasswordDto request)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return new ApiResponse<object>(false, "User not found.", null);
            }

            // verify old password
            bool isValid = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash);

            if (!isValid)
            {
                return new ApiResponse<object>(false, "Old password is incorrect.", null);
            }

            // hash new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new ApiResponse<object>(true, "Password changed successfully.", null);
        }
    }
}