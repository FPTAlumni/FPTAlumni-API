using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.AlumniService;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Alumni;
using MediaType = UniAlumni.WebAPI.Configurations.MediaType;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [Route("api/alumnus")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class AlumniController : ControllerBase
    {
        private readonly IAlumniSvc _alumniSvc;

        public AlumniController(IAlumniSvc alumniSvc)
        {
            _alumniSvc = alumniSvc;
        }

        /// <summary>
        /// [Admin] Get all alumni with condition
        /// </summary>
        /// <param name="searchAlumniModel">An object contains search and filter criteria</param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of alumni</returns>
        /// <response code="200">Returns the list of alumni</response>
        /// <response code="204">Returns if list of alumni is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(IList<GetAlumniDetail>), StatusCodes.Status200OK)]
        public IActionResult GetAllAlumnus([FromQuery] SearchAlumniModel searchAlumniModel,
            [FromQuery] PagingParam<AlumniEnum.AlumniSortCriteria> paginationModel)
        {
            IList<GetAlumniDetail> result = _alumniSvc.GetAlumniPage(paginationModel, searchAlumniModel);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }


        /// <summary>
        /// [Admin,Alumni] Get alumni by ID
        /// </summary>
        /// <param name="id">An id of alumni profile</param>
        /// <returns>List of alumni</returns>
        /// <response code="200">Returns the alumni</response>
        /// <response code="204">Returns if the alumni is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [Authorize(Roles = RolesConstants.ALUMNI)]
        [ProducesResponseType(typeof(GetAlumniDetail), 200)]
        public async Task<IActionResult> GetAlumniProfile(int id)
        {
            GetAlumniDetail result = await _alumniSvc.GetAlumniProfile(id);
            
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }


        public async Task<IActionResult> CreateAlumni([FromBody] CreateAlumniRequestBody requestBody)
        {
            var result = _alumniSvc.CreateAlumni(requestBody);

            return Created(string.Empty, result);
        }
    }
}