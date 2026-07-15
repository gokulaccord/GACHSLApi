using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;

namespace GACHSLApi.Services
{
    public class GoogleTokenService
    {
        private readonly IConfiguration _configuration;

        private const string TokenPath = "Tokens/google-token.json";

        public GoogleTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SaveToken(TokenResponse token)
        {
            if (!Directory.Exists("Tokens"))
                Directory.CreateDirectory("Tokens");

            var json = System.Text.Json.JsonSerializer.Serialize(token);

            await File.WriteAllTextAsync(TokenPath, json);
        }

        public async Task<UserCredential> GetCredentialAsync()
        {
            if (!File.Exists(TokenPath))
                throw new Exception("Google OAuth token not found.");

            var json = await File.ReadAllTextAsync(TokenPath);

            var token = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(json)
                        ?? throw new Exception("Invalid token file.");

            var flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = _configuration["GoogleOAuth:ClientId"],
                        ClientSecret = _configuration["GoogleOAuth:ClientSecret"]
                    },
                    Scopes = new[]
                    {
                        DriveService.Scope.DriveFile
                    }
                });

            var credential = new UserCredential(
                flow,
                "user",
                token);

            // Automatically refresh if needed
            await credential.RefreshTokenAsync(CancellationToken.None);

            return credential;
        }
    }
}