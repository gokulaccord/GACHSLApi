using GACHSLApi.DTOs.Notice;
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

        public async Task<List<NoticeDto>> GetAllAsync()
        {
            var notices = await _noticeRepository.GetAllAsync();

            return notices
                .Where(n => n.IsActive)
                .Select(n => new NoticeDto
                {
                    NoticeId = n.NoticeId,
                    Title = n.Title,
                    Description = n.Description,
                    PublishDate = n.PublishDate,
                    IsActive = n.IsActive
                })
                .ToList();
        }

        public async Task<NoticeDto?> GetByIdAsync(int id)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);

            if (notice == null)
                return null;

            return new NoticeDto
            {
                NoticeId = notice.NoticeId,
                Title = notice.Title,
                Description = notice.Description,
                PublishDate = notice.PublishDate,
                IsActive = notice.IsActive
            };
        }

        public async Task CreateAsync(CreateNoticeDto dto, int createdBy)
        {
            var notice = new Notice
            {
                Title = dto.Title,
                Description = dto.Description,
                PublishDate = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = createdBy,
                IsActive = true
            };

            await _noticeRepository.AddAsync(notice);
            await _noticeRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateNoticeDto dto)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);

            if (notice == null)
                throw new Exception("Notice not found.");

            notice.Title = dto.Title;
            notice.Description = dto.Description;
            notice.IsActive = dto.IsActive;

            await _noticeRepository.UpdateAsync(notice);
            await _noticeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);

            if (notice == null)
                throw new Exception("Notice not found.");

            await _noticeRepository.DeleteAsync(notice);
            await _noticeRepository.SaveChangesAsync();
        }
    }
}