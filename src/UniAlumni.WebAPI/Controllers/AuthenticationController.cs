using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.AuthenticationService;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.Token;
using MediaType = UniAlumni.WebAPI.Configurations.MediaType;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationSvc _authenticationService;

        public AuthenticationController(IAuthenticationSvc authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// [GUEST] Endpoint For Alumni Account Login
        /// </summary>
        /// <param name="tokenRequest">An object contain token and university</param>
        /// <returns>Custom Token</returns>
        /// <response code="200">Returns the custom token</response>
        /// <response code="202">Returns the UID if alumni is not exist</response>
        /// <response code="400">Return if the idToken is null</response> 
        /// <response code="401">Return if the idToken is invalid</response> 
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(typeof(String), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> LoginWithIdTokenAsync([FromBody]TokenRequest tokenRequest)
        {
            if (tokenRequest.Token == null) return BadRequest();
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(tokenRequest.Token);
                string uid = decodedToken.Uid;
                TokenResponse jwtToken = await _authenticationService.Authenticate(uid, tokenRequest.UniversityId);
                if (jwtToken.CustomToken.Length != 0)
                    return Ok(jwtToken);
                else
                    return Created(string.Empty, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized(new BaseResponse<GetAlumniDetail>()
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Data = null,
                    Msg = "Login Error!, Please try again!" + e
                });
            }
        }
    }
}