namespace GACHSLApi.DTOs.Notice
{
    public class UpdateNoticeDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}