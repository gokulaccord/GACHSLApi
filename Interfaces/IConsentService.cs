using GACHSLApi.Common;
using GACHSLApi.DTOs.Consent;

namespace GACHSLApi.Interfaces
{
    public interface IConsentService
    {
        Task<ApiResponse<List<ConsentDto>>> GetAllAsync();

        Task<ApiResponse<ConsentDto>> GetByIdAsync(int id);

        Task<ApiResponse<object>> CreateAsync(CreateConsentDto dto);

        Task<ApiResponse<object>> UpdateAsync(int id, UpdateConsentDto dto);

        Task<ApiResponse<object>> DeleteAsync(int id);
    }
}