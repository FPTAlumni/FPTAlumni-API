using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniAlumni.Business.Services.Interface;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _url;
        private readonly IAuthenticationSvc _authenticationService;
        private readonly FirebaseAuth _firebaseAuth;

        public AuthenticationController(IAuthenticationSvc authenticationService)
        {
            _url = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";
            _authenticationService = authenticationService;
            _firebaseAuth = FirebaseAuth.DefaultInstance;
        }

        [HttpPost("accessToken")]
        public async Task<IActionResult> ParseAccessTokenAsync(string accessToken)
        {
            if (accessToken == null) return BadRequest();
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(_url + accessToken);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    FirebaseResponse responseData = await JsonSerializer.DeserializeAsync<FirebaseResponse>(dataStream);
                    string jwtToken = _authenticationService.Authenticate(responseData.user_id);
                    if (jwtToken.Length != 0)
                        return Ok(new {token = jwtToken, message = "Login success"});
                    else
                        return Ok(new {message = "Alumni has not been register"});
                }
            }
            catch (WebException)
            {
                return Unauthorized();
            }
            finally
            {
                if (response != null) response.Close();
            }
        }

        [HttpPost("token")]
        public async Task<IActionResult> ParseIdTokenAsync(string idToken)
        {
            if (idToken == null) return BadRequest();
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(idToken);
                string uid = decodedToken.Uid;
                string jwtToken = _authenticationService.Authenticate(uid);
                if (jwtToken.Length != 0)
                    return Ok(new {token = jwtToken, message = "Login success"});
                else
                    return Ok(new {message = "Alumni has not been register"});
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

    public class FirebaseResponse
    {
        public string issued_to { get; set; }
        public string audience { get; set; }
        public string user_id { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string email { get; set; }
        public bool verified_email { get; set; }
        public string access_type { get; set; }
    }
}