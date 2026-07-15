namespace GACHSLApi.DTOs.Document
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string GoogleDriveFileId { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public string? ViewUrl { get; set; }

        public string? DownloadUrl { get; set; }

        public string? FileName { get; set; }

        public string? FileExtension { get; set; }

        public long FileSize { get; set; }

        public string? MimeType { get; set; }

    }
}