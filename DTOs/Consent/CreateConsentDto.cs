using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.Consent
{
    public class CreateConsentDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public byte ConsentStatus { get; set; }

        public DateTime? ConsentDate { get; set; }

        public string? Remarks { get; set; }

        public int? DocumentId { get; set; }
    }
}