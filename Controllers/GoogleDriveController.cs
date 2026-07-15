using GACHSLApi.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GACHSLApi.Controllers
{
    [Route("api/google")]
    [ApiController]
    public class GoogleDriveController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly GoogleTokenService _tokenService;

        public GoogleDriveController(
            IConfiguration configuration,
            GoogleTokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }


        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            var clientId =
                _configuration["GoogleOAuth:ClientId"]
                ?? throw new Exception("ClientId missing");

            var clientSecret =
                _configuration["GoogleOAuth:ClientSecret"]
                ?? throw new Exception("ClientSecret missing");


            var flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    Scopes = new[]
                    {
                        "https://www.googleapis.com/auth/drive.file"
                    }
                });


            TokenResponse token =
                await flow.ExchangeCodeForTokenAsync(
                    "user",
                    code,
                    "http://localhost:5278/api/google/callback",
                    CancellationToken.None);


            await _tokenService.SaveToken(token);


            return Ok(new
            {
                Success = true,
                Message = "Google Drive connected successfully"
            });
        }
        [HttpGet("connect")]
        public IActionResult Connect()
        {
            var clientId = _configuration["GoogleOAuth:ClientId"];

            var redirectUri =
                "http://localhost:5278/api/google/callback";

            var scope =
                "https://www.googleapis.com/auth/drive.file";


            var url =
                "https://accounts.google.com/o/oauth2/v2/auth" +
                "?client_id=" + clientId +
                "&redirect_uri=" + Uri.EscapeDataString(redirectUri) +
                "&response_type=code" +
                "&scope=" + Uri.EscapeDataString(scope) +
                "&access_type=offline" +
                "&prompt=consent";


            return Redirect(url);
        }
    }
}