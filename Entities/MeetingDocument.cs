using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GACHSLApi.Entities
{
    public class MeetingDocument
    {
        [Key]
        public int Id { get; set; }

        public int MeetingId { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public string? OriginalFileName { get; set; }

        [Required]
        public string GoogleDriveFileId { get; set; } = string.Empty;

        public string? MimeType { get; set; }

        public long? FileSize { get; set; }

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(MeetingId))]
        public Meeting? Meeting { get; set; }
    }
}