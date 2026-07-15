namespace GACHSLApi.DTOs
{
    public class GoogleDriveUploadResultDto
    {
        public string FileId { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string MimeType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public string ViewUrl { get; set; } = string.Empty;

        public string DownloadUrl { get; set; } = string.Empty;
    }
}