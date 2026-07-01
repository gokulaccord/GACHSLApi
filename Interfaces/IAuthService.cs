using GACHSLApi.Common;
using GACHSLApi.DTOs;

namespace GACHSLApi.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> RegisterAsync(RegisterRequestDto request);

        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
        Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}