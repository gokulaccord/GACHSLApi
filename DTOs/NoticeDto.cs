namespace GACHSLApi.DTOs.Notice
{
    public class NoticeDto
    {
        public int NoticeId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public bool IsActive { get; set; }
    }
}