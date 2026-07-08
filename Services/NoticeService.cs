using GACHSLApi.Common;
using GACHSLApi.DTOs;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class NoticeService : INoticeService
    {
        private readonly INoticeRepository _noticeRepository;

        public NoticeService(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        public async Task<ApiResponse<List<NoticeDto>>> GetAllAsync()
        {
            var notices = await _noticeRepository.GetAllAsync();

            var result = notices.Select(n => new NoticeDto
            {
                NoticeId = n.NoticeId,
                Title = n.Title,
                Description = n.Description,
                PublishDate = n.PublishDate,
                Category = n.Category,
                Priority = n.Priority,
                AttachmentUrl = n.AttachmentUrl,
                IsPublished = n.IsPublished,
                IsActive = n.IsActive,
                CreatedOn = n.CreatedOn,
                UpdatedOn = n.UpdatedOn,
                CreatedBy = n.CreatedBy
            }).ToList();

            return new ApiResponse<List<NoticeDto>>(true, "Success", result);
        }

        public async Task<ApiResponse<NoticeDto>> GetByIdAsync(int id)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);

            if (notice == null)
            {
                return new ApiResponse<NoticeDto>(
                    false,
                    "Notice not found.",
                    null);
            }

            var dto = new NoticeDto
            {
                NoticeId = notice.NoticeId,
                Title = notice.Title,
                Description = notice.Description,
                PublishDate = notice.PublishDate,
                Category = notice.Category,
                Priority = notice.Priority,
                AttachmentUrl = notice.AttachmentUrl,
                IsPublished = notice.IsPublished,
                IsActive = notice.IsActive,
                CreatedOn = notice.CreatedOn,
                UpdatedOn = notice.UpdatedOn,
                CreatedBy = notice.CreatedBy
            };

            return new ApiResponse<NoticeDto>(true, "Success", dto);
        }

        public async Task<ApiResponse<object>> CreateAsync(CreateNoticeDto dto)
        {
            var notice = new Notice
            {
                Title = dto.Title,
                Description = dto.Description,
                PublishDate = dto.PublishDate,
                Category = dto.Category,
                Priority = dto.Priority,
                AttachmentUrl = dto.AttachmentUrl,
                IsPublished = dto.IsPublished,
                IsActive = dto.IsActive,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy
            };

            await _noticeRepository.AddAsync(notice);
            await _noticeRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Notice created successfully.",
                null);
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, UpdateNoticeDto dto)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);

            if (notice == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Notice not found.",
                    null);
            }

            notice.Title = dto.Title;
            notice.Description = dto.Description;
            notice.PublishDate = dto.PublishDate;
            notice.Category = dto.Category;
            notice.Priority = dto.Priority;
            notice.AttachmentUrl = dto.AttachmentUrl;
            notice.IsPublished = dto.IsPublished;
            notice.IsActive = dto.IsActive;
            notice.UpdatedOn = DateTime.UtcNow;

            await _noticeRepository.UpdateAsync(notice);
            await _noticeRepository.SaveChangesAsync();
            return new ApiResponse<object>(
                true,
                "Notice updated successfully.",
                null);
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);

            if (notice == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Notice not found.",
                    null);
            }

            await _noticeRepository.DeleteAsync(notice);
            await _noticeRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Notice deleted successfully.",
                null);
        }
    }
}