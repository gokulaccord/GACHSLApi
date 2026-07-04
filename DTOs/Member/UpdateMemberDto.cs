using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.Member
{
    public class UpdateMemberDto
    {
        [Required]
        public string FlatNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool IsOwner { get; set; }

        public bool IsActive { get; set; }
    }
}