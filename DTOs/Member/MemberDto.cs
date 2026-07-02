namespace GACHSLApi.DTOs.Member
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string FlatNumber { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; }
    }
}