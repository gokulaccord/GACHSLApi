using System.ComponentModel.DataAnnotations;

namespace GACHSLApi.Entities
{
    public class DocumentCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}