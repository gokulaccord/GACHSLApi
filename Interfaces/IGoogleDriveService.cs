using GACHSLApi.DTOs;
using Microsoft.AspNetCore.Http;

namespace GACHSLApi.Interfaces
{
    public interface IGoogleDriveService
    {
        Task<string> UploadFileAsync(IFormFile file);

        Task<Stream> DownloadFileAsync(string fileId);

        Task DeleteFileAsync(string fileId);
        string GetViewUrl(string fileId);

        string GetDownloadUrl(string fileId);
    }
}