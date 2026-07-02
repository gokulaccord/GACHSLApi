using GACHSLApi.Common;
using GACHSLApi.DTOs.Member;

namespace GACHSLApi.Interfaces
{
    public interface IMemberService
    {
        Task<ApiResponse<List<MemberDto>>> GetAllAsync();
        Task<ApiResponse<MemberDto>> GetByIdAsync(int id);
        Task<ApiResponse<object>> CreateAsync(CreateMemberDto dto);
        Task<ApiResponse<object>> UpdateAsync(int id, UpdateMemberDto dto);
        Task<ApiResponse<object>> DeleteAsync(int id);
    }
}