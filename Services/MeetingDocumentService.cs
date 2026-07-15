using GACHSLApi.Common;
using GACHSLApi.DTOs.MeetingDocument;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class MeetingDocumentService : IMeetingDocumentService
    {
        private readonly IMeetingDocumentRepository _meetingDocumentRepository;
        private readonly IGoogleDriveService _googleDriveService;

        public MeetingDocumentService(
            IMeetingDocumentRepository meetingDocumentRepository,
            IGoogleDriveService googleDriveService)
        {
            _meetingDocumentRepository = meetingDocumentRepository;
            _googleDriveService = googleDriveService;
        }

        public async Task<ApiResponse<List<MeetingDocumentDto>>> GetByMeetingIdAsync(int meetingId)
        {
            var documents = await _meetingDocumentRepository.GetByMeetingIdAsync(meetingId);

            var result = documents.Select(document => new MeetingDocumentDto
            {
                Id = document.Id,
                MeetingId = document.MeetingId,
                FileName = document.FileName,
                OriginalFileName = document.OriginalFileName,
                GoogleDriveFileId = document.GoogleDriveFileId,
                MimeType = document.MimeType,
                FileSize = document.FileSize,
                Description = document.Description,
                DisplayOrder = document.DisplayOrder,

                ViewUrl = _googleDriveService.GetViewUrl(document.GoogleDriveFileId),
                DownloadUrl = _googleDriveService.GetDownloadUrl(document.GoogleDriveFileId)
            }).ToList();

            return new ApiResponse<List<MeetingDocumentDto>>
            (
                true,
                "Meeting documents retrieved successfully.",
                result
            );
        }

        public async Task<ApiResponse<object>> CreateAsync(
    CreateMeetingDocumentDto dto,
    int createdBy)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                return new ApiResponse<object>(
                    false,
                    "Please select a file.",
                    null);
            }

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

            string fileId = await _googleDriveService.UploadFileAsync(dto.File);

            var document = new MeetingDocument
            {
                MeetingId = dto.MeetingId,

                FileName = dto.File.FileName,
                OriginalFileName = dto.File.FileName,

                GoogleDriveFileId = fileId,

                MimeType = dto.File.ContentType,
                FileSize = dto.File.Length,

                Description = dto.Description,

                DisplayOrder = dto.DisplayOrder,

                IsActive = true,

                CreatedBy = createdBy,
                CreatedOn = DateTime.UtcNow
            };

            await _meetingDocumentRepository.AddAsync(document);
            await _meetingDocumentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting document uploaded successfully.",
                null);
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var document = await _meetingDocumentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Meeting document not found.",
                    null);
            }

            await _googleDriveService.DeleteFileAsync(document.GoogleDriveFileId);

            await _meetingDocumentRepository.DeleteAsync(document);
            await _meetingDocumentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Meeting document deleted successfully.",
                null);
        }
    }
}