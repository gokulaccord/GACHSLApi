using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.Consent
{
    public class UpdateConsentDto
    {
        [Required]
        public byte ConsentStatus { get; set; }

        public DateTime? ConsentDate { get; set; }

        public string? Remarks { get; set; }

        public int? DocumentId { get; set; }

        public bool IsActive { get; set; }
    }
}