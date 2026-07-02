using GACHSLApi.Common;
using GACHSLApi.DTOs;

namespace GACHSLApi.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserResponseDto>> GetCurrentUserAsync(int userId);

        Task<ApiResponse<List<UserResponseDto>>> GetAllUsersAsync(UserQueryDto query);

        Task<ApiResponse<object>> ChangePasswordAsync(int userId, ChangePasswordDto request);
    }
}