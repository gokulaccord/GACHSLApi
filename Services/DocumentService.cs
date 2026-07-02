using GACHSLApi.Common;
using GACHSLApi.DTOs.Document;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;
using GACHSLApi.Helpers;

namespace GACHSLApi.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<ApiResponse<List<DocumentDto>>> GetAllAsync(DocumentQueryDto query)
        {
            var documents = await _documentRepository.GetAllAsync();

            // Filter by Category
            if (query.CategoryId.HasValue)
            {
                documents = documents
                    .Where(d => d.CategoryId == query.CategoryId.Value)
                    .ToList();
            }

            // Filter by Active
            if (query.IsActive.HasValue)
            {
                documents = documents
                    .Where(d => d.IsActive == query.IsActive.Value)
                    .ToList();
            }

            var result = documents
     .Select(DocumentMapper.ToDto)
     .ToList();

            return new ApiResponse<List<DocumentDto>>(
                true,
                "Documents retrieved successfully.",
                result);
        }
        public async Task<ApiResponse<DocumentDto>> GetByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return new ApiResponse<DocumentDto>(
                    false,
                    "Document not found.",
                    null);
            }

            var dto = DocumentMapper.ToDto(document);

            return new ApiResponse<DocumentDto>(
                true,
                "Document retrieved successfully.",
                dto);
        }
        public async Task<ApiResponse<object>> CreateAsync(CreateDocumentDto dto, int createdBy)
        {
            var document = new Document
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                GoogleDriveFileId = dto.GoogleDriveFileId,
                PublishDate = dto.PublishDate,
                DisplayOrder = dto.DisplayOrder,
                IsActive = true,

                // IMPORTANT
                CreatedBy = createdBy,
                CreatedOn = DateTime.UtcNow
            };

            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Document created successfully.",
                null);
        }
        public async Task<ApiResponse<object>> UpdateAsync(int id, UpdateDocumentDto dto)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Document not found.",
                    null);
            }

            // Update allowed fields only (Option A rule)
            document.Title = dto.Title;
            document.Description = dto.Description;
            document.CategoryId = dto.CategoryId;
            document.GoogleDriveFileId = dto.GoogleDriveFileId;
            document.PublishDate = dto.PublishDate;
            document.DisplayOrder = dto.DisplayOrder;
            document.IsActive = dto.IsActive;

            await _documentRepository.UpdateAsync(document);
            await _documentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Document updated successfully.",
                null);
        }
        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Document not found.",
                    null);
            }

            await _documentRepository.DeleteAsync(document);
            await _documentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Document deleted successfully.",
                null);
        }
    }
    
}