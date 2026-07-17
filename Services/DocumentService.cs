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
        private readonly IGoogleDriveService _googleDriveService;

        public DocumentService(
            IDocumentRepository documentRepository,
            IGoogleDriveService googleDriveService)
        {
            _documentRepository = documentRepository;
            _googleDriveService = googleDriveService;
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
            foreach (var d in documents)
            {
                Console.WriteLine($"DB -> ID={d.DocumentId}, GoogleDriveFileId='{d.GoogleDriveFileId}'");
            }
            var result = documents.Select(document => new DocumentDto
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                Description = document.Description,

                CategoryId = document.CategoryId,
                CategoryName = document.Category?.CategoryName,

                GoogleDriveFileId = document.GoogleDriveFileId,

                PublishDate = document.PublishDate,
                DisplayOrder = document.DisplayOrder,
                IsActive = document.IsActive,

                FileName = document.FileName,
                FileExtension = document.FileExtension,
                MimeType = document.MimeType,
                FileSize = document.FileSize,

                ViewUrl = _googleDriveService.GetViewUrl(document.GoogleDriveFileId),
                DownloadUrl = _googleDriveService.GetDownloadUrl(document.GoogleDriveFileId)
            }).ToList();

            foreach (var doc in result)
            {
                doc.ViewUrl = _googleDriveService.GetViewUrl(doc.GoogleDriveFileId);

                doc.DownloadUrl = _googleDriveService.GetDownloadUrl(doc.GoogleDriveFileId);
            }

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

            var dto = new DocumentDto
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                Description = document.Description,

                CategoryId = document.CategoryId,
                CategoryName = document.Category?.CategoryName,

                GoogleDriveFileId = document.GoogleDriveFileId,

                PublishDate = document.PublishDate,
                DisplayOrder = document.DisplayOrder,
                IsActive = document.IsActive,

                FileName = document.FileName,
                FileExtension = document.FileExtension,
                MimeType = document.MimeType,
                FileSize = document.FileSize,

                ViewUrl = _googleDriveService.GetViewUrl(document.GoogleDriveFileId),
                DownloadUrl = _googleDriveService.GetDownloadUrl(document.GoogleDriveFileId)
            };

            dto.ViewUrl = _googleDriveService.GetViewUrl(dto.GoogleDriveFileId);

            dto.DownloadUrl = _googleDriveService.GetDownloadUrl(dto.GoogleDriveFileId);

            return new ApiResponse<DocumentDto>(
                true,
                "Document retrieved successfully.",
                dto);
        }
        public async Task<ApiResponse<object>> CreateAsync(
     CreateDocumentDto dto,
     int createdBy)
        {
            // Validate file
            if (dto.File == null || dto.File.Length == 0)
            {
                return new ApiResponse<object>(
                    false,
                    "Please select a file.",
                    null);
            }
            // Validate file type
            var allowedExtensions = new[]
            {
        ".pdf",
        ".doc",
        ".docx",
        ".xls",
        ".xlsx",
        ".jpg",
        ".jpeg",
        ".png"
    };

            var extension = Path.GetExtension(dto.File.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return new ApiResponse<object>(
                    false,
                    "Only PDF, Word, Excel and Image files are allowed.",
                    null);
            }
            string fileId;

            try
            {
                Console.WriteLine("Uploading PDF to Google Drive...");

                fileId = await _googleDriveService.UploadFileAsync(dto.File);

                Console.WriteLine("Google Drive File ID: " + fileId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Google Drive Error:");
                Console.WriteLine(ex.ToString());

                throw;
            }
            var document = new Document
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,

                GoogleDriveFileId = fileId,

                FileName = dto.File.FileName,
                FileExtension = Path.GetExtension(dto.File.FileName),
                MimeType = dto.File.ContentType,
                FileSize = dto.File.Length,

                PublishDate = dto.PublishDate,
                DisplayOrder = dto.DisplayOrder,

                IsActive = true,

                CreatedBy = createdBy,
                CreatedOn = DateTime.UtcNow
            };

            try
            {
                Console.WriteLine("Saving document to database...");

                await _documentRepository.AddAsync(document);
                await _documentRepository.SaveChangesAsync();

                Console.WriteLine("Database save successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error:");
                Console.WriteLine(ex.ToString());

                throw;
            }

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
            //document.GoogleDriveFileId = dto.GoogleDriveFileId;
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

            await _googleDriveService.DeleteFileAsync(document.GoogleDriveFileId);

            await _documentRepository.DeleteAsync(document);
            await _documentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Document deleted successfully.",
                null);
        }
        public async Task<Stream> DownloadFileAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);

            if (document == null)
            {
                throw new Exception("Document not found.");
            }

            return await _googleDriveService.DownloadFileAsync(document.GoogleDriveFileId);
        }
        public async Task<List<DocumentLookupDto>> GetLookupAsync()
        {
            var documents = await _documentRepository.GetLookupAsync();

            return documents.Select(d => new DocumentLookupDto
            {
                DocumentId = d.DocumentId,
                Title = d.Title
            }).ToList();
        }
    }
    
}