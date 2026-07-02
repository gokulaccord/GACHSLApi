namespace GACHSLApi.DTOs.Member
{
    public class UpdateMemberDto
    {
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; }
    }
}