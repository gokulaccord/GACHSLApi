namespace GACHSLApi.DTOs.MeetingDocument
{
    public class MeetingDocumentDto
    {
        public int Id { get; set; }

        public int MeetingId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string? OriginalFileName { get; set; }

        public string GoogleDriveFileId { get; set; } = string.Empty;

        public string? MimeType { get; set; }

        public long? FileSize { get; set; }

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public string? ViewUrl { get; set; }

        public string? DownloadUrl { get; set; }
    }
}