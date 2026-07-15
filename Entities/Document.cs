using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GACHSLApi.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CategoryId { get; set; }
       

        public string GoogleDriveFileId { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string FileExtension { get; set; } = string.Empty;

        public string MimeType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime PublishDate { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsPublished { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public DocumentCategory? Category { get; set; }
    }
}