using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using UniAlumni.Business.Services.Interface;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [Route("api/alumnus")]
    
    public class AlumniController : ControllerBase
    {
        private readonly IAlumniSvc _alumniSvc;

        public AlumniController(IAlumniSvc alumniSvc)
        {
            _alumniSvc = alumniSvc;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllAlumnus()
        {
            var listAlumnus = _alumniSvc.GetAlumnis();
            return Ok(listAlumnus);
        }
        
        
        
    }
}