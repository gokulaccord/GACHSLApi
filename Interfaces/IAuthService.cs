using GACHSLApi.Common;
using GACHSLApi.DTOs;

namespace GACHSLApi.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> RegisterAsync(RegisterRequestDto request);

        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);

        Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);

        Task<ApiResponse<object>> LogoutAsync(RefreshTokenRequestDto request);
        Task<ApiResponse<object>> ForgotPasswordAsync(ForgotPasswordDto request);
        Task<ApiResponse<object>> VerifyOtpAsync(VerifyOtpDto request);
        Task<ApiResponse<object>> ResetPasswordAsync(ResetPasswordDto request);
    }
}