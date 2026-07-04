namespace GACHSLApi.DTOs.SocietySettings
{
    public class SocietySettingsDto
    {
        public int Id { get; set; }

        public string SocietyName { get; set; } = string.Empty;

        public string? SocietyShortName { get; set; }

        public int TotalFlats { get; set; }

        public int TotalShops { get; set; }

        public int CurrentStage { get; set; }

        public int TotalStages { get; set; }

        public string? PMCName { get; set; }

        public string? DeveloperName { get; set; }

        public string? RegistrationNumber { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? LogoUrl { get; set; }
    }
}