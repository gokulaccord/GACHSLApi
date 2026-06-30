using GACHSLApi.Common;
using GACHSLApi.DTOs;

namespace GACHSLApi.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDto request);

        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}