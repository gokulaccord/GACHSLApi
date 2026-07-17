using GACHSLApi.Common;
using GACHSLApi.DTOs.Document;

namespace GACHSLApi.Interfaces
{
    public interface IDocumentService
    {
        Task<ApiResponse<List<DocumentDto>>> GetAllAsync(DocumentQueryDto query);

        Task<ApiResponse<DocumentDto>> GetByIdAsync(int id);

        Task<ApiResponse<object>> CreateAsync(CreateDocumentDto dto, int createdBy);

        Task<ApiResponse<object>> UpdateAsync(int id, UpdateDocumentDto dto);

        Task<ApiResponse<object>> DeleteAsync(int id);

        Task<Stream> DownloadFileAsync(int documentId);

        // New
        Task<List<DocumentLookupDto>> GetLookupAsync();
    }
}