using System;
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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/alumnus")]
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
        /// [Admin] Endpoint for get all alumni with condition
        /// </summary>
        /// <param name="searchUniversityModel">An object contains search and filter criteria</param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of alumni</returns>
        /// <response code="200">Returns the list of alumni</response>
        /// <response code="204">Returns if list of alumni is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(IList<GetAlumniDetail>), StatusCodes.Status200OK)]
        public IActionResult GetAllAlumnus([FromQuery] SearchAlumniModel searchUniversityModel,
            [FromQuery] PagingParam<AlumniEnum.AlumniSortCriteria> paginationModel)
        {
            IList<GetAlumniDetail> result = _alumniSvc.GetAlumniPage(paginationModel, searchUniversityModel);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }


        /// <summary>
        /// [Admin,Alumni] Endpoint for get alumni by ID
        /// </summary>
        /// <param name="id">An id of alumni profile</param>
        /// <returns>List of alumni</returns>
        /// <response code="200">Returns the alumni</response>
        /// <response code="204">Returns if the alumni is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(GetAlumniDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlumniProfile(int id)
        {
            GetAlumniDetail result = await _alumniSvc.GetAlumniProfile(id);
            
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }

        /// <summary>
        /// [Guest] Endpoint for create pending alumni
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an Alumni.</param>
        /// <returns>A Alumni within status 201 or error status.</returns>
        /// <response code="201">Returns the alumni</response>
        /// <response code="204">Returns if the alumni is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetAlumniDetail), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAlumni([FromBody] CreateAlumniRequestBody requestBody)
        {
            var result = await _alumniSvc.CreateAlumniAsync(requestBody);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// [Admin,Alumni] Endpoint for Alumni or Admin edit profile.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an Alumni.</param>
        /// <returns>A Alumni within status 200 or error status.</returns>
        /// <response code="200">Returns alumni after update</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(GetAlumniDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAlumni([FromBody] UpdateAlumniRequestBody requestBody)
        {
            GetAlumniDetail updateAlumni = await _alumniSvc.UpdateAlumniAsync(requestBody);

            return Ok(updateAlumni);
        }

        /// <summary>
        /// [Admin] Endpoint to activate or reject Alumni.
        /// </summary>
        /// <param name="requestBody">An obj contains Uid info and status of an Alumni.</param>
        /// <returns>204 status.</returns>
        /// <response code="204">Returns NoContent status</response>
        [HttpPatch("activate")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ActiveAlumni([FromQuery] ActivateAlumniRequestBody requestBody)
        {
            await _alumniSvc.ActivateAlumniAsync(requestBody);

            return NoContent();
        }

        /// <summary>
        /// [Admin] Endpoint for Admin Deactivation a alumni.
        /// </summary>
        /// <param name="id">ID of alumni</param>
        /// <returns>A Alumni within status 200 or 204 status.</returns>
        /// <response code="200">Returns 200 status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteAlumniAsync(int id)
        {
            try
            {
                await _alumniSvc.DeleteAlumniAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NoContent();
            }
            return Ok();
        }




    }
}