using GACHSLApi.Interfaces;
using GACHSLApi.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.Extensions.Options;

namespace GACHSLApi.Services
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly DriveService _driveService;
        private readonly GoogleDriveSettings _settings;

        public GoogleDriveService(
            GoogleTokenService tokenService,
            IOptions<GoogleDriveSettings> options)
        {
            _settings = options.Value;

            var credential = tokenService.GetCredentialAsync()
                .GetAwaiter()
                .GetResult();


            _driveService = new DriveService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "GACHSL Portal"
                });
        }


        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);

            var fileName =
                $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now:yyyyMMddHHmmss}{extension}";


            var metadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,

                Parents = new List<string>
                {
                    _settings.FolderId
                }
            };


            using var stream = file.OpenReadStream();


            var request =
                _driveService.Files.Create(
                    metadata,
                    stream,
                    file.ContentType);


            request.Fields = "id,name,webViewLink";

            var result = await request.UploadAsync();


            if (result.Status != UploadStatus.Completed)
            {
                throw new Exception(
                    result.Exception?.Message);
            }


            return request.ResponseBody.Id;
        }


        public async Task<Stream> DownloadFileAsync(string fileId)
        {
            var stream = new MemoryStream();

            var request =
                _driveService.Files.Get(fileId);


            await request.DownloadAsync(stream);

            stream.Position = 0;

            return stream;
        }


        public async Task DeleteFileAsync(string fileId)
        {
            await _driveService.Files.Delete(fileId)
                .ExecuteAsync();
        }
        public string GetViewUrl(string fileId)
        {
            return $"https://drive.google.com/file/d/{fileId}/view";
        }

        public string GetDownloadUrl(string fileId)
        {
            return $"https://drive.google.com/uc?export=download&id={fileId}";
        }
    }
}