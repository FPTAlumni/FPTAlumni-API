using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.AuthenticationService;
using UniAlumni.WebAPI.DTO.Token;
using MediaType = UniAlumni.WebAPI.Configurations.MediaType;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
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
        /// <param name="idToken">Authentication Token Get From Firebase Service</param>
        /// <returns>Custom Token</returns>
        /// <response code="200">Returns the custom token</response>
        /// <response code="201">Returns the UID if alumni is not exist</response>
        /// <response code="400">Return if the idToken is null</response> 
        /// <response code="401">Return if the idToken is invalid</response> 
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(typeof(String), StatusCodes.Status201Created)]
        public async Task<IActionResult> LoginWithIdTokenAsync(string idToken)
        {
            if (idToken == null) return BadRequest();
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(idToken);
                string uid = decodedToken.Uid;
                string jwtToken = _authenticationService.Authenticate(uid);
                if (jwtToken.Length != 0)
                    return Ok(TokenResponse.BuildTokenResponse(jwtToken));
                else
                    return Ok(uid);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}