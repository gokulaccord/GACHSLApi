using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GACHSLApi.Entities
{
    public class Consent
    {
        [Key]
        public int ConsentId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public byte ConsentStatus { get; set; } = 0;

        public DateTime? ConsentDate { get; set; }

        [MaxLength(1000)]
        public string? Remarks { get; set; }

        public int? DocumentId { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Member? Member { get; set; }

        [ForeignKey(nameof(DocumentId))]
        public virtual Document? Document { get; set; }
    }
}