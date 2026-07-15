using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.DTOs.MeetingDocument
{
    public class CreateMeetingDocumentDto
    {
        [Required]
        public int MeetingId { get; set; }

        [Required]
        public IFormFile File { get; set; } = null!;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}