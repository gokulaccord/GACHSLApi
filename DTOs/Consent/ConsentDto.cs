namespace GACHSLApi.DTOs.Consent
{
    public class ConsentDto
    {
        public int ConsentId { get; set; }

        public int MemberId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public string FlatNumber { get; set; } = string.Empty;

        public byte ConsentStatus { get; set; }

        public DateTime? ConsentDate { get; set; }

        public string? Remarks { get; set; }

        public int? DocumentId { get; set; }

        public bool IsActive { get; set; }
    }
}