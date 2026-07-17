namespace GACHSLApi.DTOs.Member
{
    public class MemberLookupDto
    {
        public int MemberId { get; set; }

        public string FlatNumber { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }
}