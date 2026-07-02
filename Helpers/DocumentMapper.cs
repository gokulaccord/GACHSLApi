using GACHSLApi.DTOs.Document;
using GACHSLApi.Entities;

namespace GACHSLApi.Helpers
{
    public static class DocumentMapper
    {
        public static DocumentDto ToDto(Document document)
        {
            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                Description = document.Description,
                CategoryId = document.CategoryId,
                CategoryName = document.Category?.CategoryName ?? "",
                GoogleDriveFileId = document.GoogleDriveFileId,
                PublishDate = document.PublishDate,
                DisplayOrder = document.DisplayOrder,
                IsActive = document.IsActive
            };
        }
    }
}