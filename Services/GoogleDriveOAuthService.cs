using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace GACHSLApi.Services
{
    public class GoogleDriveOAuthService
    {
        private readonly IConfiguration _configuration;

        public GoogleDriveOAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public DriveService GetDriveService()
        {
            var clientId = _configuration["GoogleOAuth:ClientId"];
            var clientSecret = _configuration["GoogleOAuth:ClientSecret"];

            var credential = new UserCredential(
                new GoogleAuthorizationCodeFlow(
                    new GoogleAuthorizationCodeFlow.Initializer
                    {
                        ClientSecrets = new ClientSecrets
                        {
                            ClientId = clientId,
                            ClientSecret = clientSecret
                        },
                        Scopes = new[]
                        {
                            DriveService.Scope.DriveFile
                        }
                    }),
                "user",
                new Google.Apis.Auth.OAuth2.Responses.TokenResponse()
            );


            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GACHSL Portal"
            });
        }
        public async Task<string> UploadFile(
    Stream fileStream,
    string fileName)
        {
            var service = GetDriveService();

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string>
{
    _configuration["GoogleDrive:FolderId"]
        ?? throw new Exception("Google Drive FolderId missing in appsettings.json")
}
            };


            var request = service.Files.Create(
                fileMetadata,
                fileStream,
                "application/octet-stream"
            );

            request.Fields = "id";

            await request.UploadAsync();

            var file = request.ResponseBody;

            return file.Id;
        }
    }
}