namespace GACHSLApi.Entities
{
    public class PasswordResetOtp
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Otp { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiryOn { get; set; }

        public bool IsUsed { get; set; }

        public User User { get; set; } = null!;
    }
}