using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.PushNotification;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/notification")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class Notification : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Send(string uid, string title, string content)
        {
            await PushNotification.SendMessage(uid, title, content);
            return Ok();
        }
    }
}