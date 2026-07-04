using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.Entities
{
    public class SocietySettings: BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string SocietyName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? SocietyShortName { get; set; }

        public int TotalFlats { get; set; }

        public int TotalShops { get; set; }

        public int CurrentStage { get; set; }

        public int TotalStages { get; set; } = 8;

        [MaxLength(200)]
        public string? PMCName { get; set; }

        [MaxLength(200)]
        public string? DeveloperName { get; set; }

        [MaxLength(100)]
        public string? RegistrationNumber { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }
    }
}