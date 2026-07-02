using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.Entities
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [MaxLength(10)]
        public string FlatNumber { get; set; } = string.Empty; // C-101

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public bool IsOwner { get; set; } = true;

        public bool IsActive { get; set; } = true;

        // Link to login user (important for authentication mapping)
        public int? UserId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}