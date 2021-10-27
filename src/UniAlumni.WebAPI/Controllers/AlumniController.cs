using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.AlumniGroupService;
using UniAlumni.Business.Services.AlumniService;
using UniAlumni.Business.Services.EventRegistrationService;
using UniAlumni.DataTier.Common;
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
        private readonly IAlumniGroupSvc _alumniGroupSvc;
        private readonly IEventRegistrationSvc _eventRegistrationSvc;

        public AlumniController(IAlumniSvc alumniSvc, IAlumniGroupSvc alumniGroupSvc, IEventRegistrationSvc eventRegistrationSvc)
        {
            _eventRegistrationSvc = eventRegistrationSvc;
            _alumniGroupSvc = alumniGroupSvc;
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
        [MapToApiVersion("1.0")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(ModelsResponse<GetAlumniDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAlumnus([FromQuery] SearchAlumniModel searchUniversityModel,
            [FromQuery] PagingParam<AlumniEnum.AlumniSortCriteria> paginationModel)
        {
            IList<GetAlumniDetail> result = _alumniSvc.GetAlumniPage(paginationModel, searchUniversityModel);
            int total = await _alumniSvc.GetTotal();
            
            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(new ModelsResponse<GetAlumniDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result.ToList(),
                Metadata = new PagingMetadata()
                {
                    Page = paginationModel.Page,
                    Size = paginationModel.PageSize,
                    Total = total
                },
                Msg = "OK"
            });
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
        [ProducesResponseType(typeof(BaseResponse<GetAlumniDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlumniProfile(int id)
        {
            GetAlumniDetail result = await _alumniSvc.GetAlumniProfile(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(new BaseResponse<GetAlumniDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result,
                Msg = ""
            });
        }

        /// <summary>
        /// [Guest] Endpoint for create pending alumni
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an Alumni.</param>
        /// <returns>A Alumni within status 201 or error status.</returns>
        /// <response code="201">Returns the alumni</response>
        /// <response code="404">Return if create is fail</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<GetAlumniDetail>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAlumni([FromBody] CreateAlumniRequestBody requestBody)
        {
            try
            {
                var result = await _alumniSvc.CreateAlumniAsync(requestBody);

                return Created(string.Empty, new BaseResponse<GetAlumniDetail>()
                {
                    Code = StatusCodes.Status201Created,
                    Data = result,
                    Msg = ""
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// [Admin,Alumni] Endpoint for Alumni or Admin edit profile.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an Alumni.</param>
        /// <returns>A Alumni within status 200 or error status.</returns>
        /// <response code="200">Returns alumni after update</response>
        /// <response code="403">Return if token is access denied</response>
        /// <response code="404">Return if alumni is not exist</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(BaseResponse<GetAlumniDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAlumni([FromBody] UpdateAlumniRequestBody requestBody)
        {
            try
            {
                GetAlumniDetail updateAlumni = await _alumniSvc.UpdateAlumniAsync(requestBody);

                return Ok(new BaseResponse<GetAlumniDetail>()
                {
                    Code = StatusCodes.Status200OK,
                    Data = updateAlumni,
                    Msg = ""
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// [Admin] Endpoint to activate or reject Alumni.
        /// </summary>
        /// <param name="requestBody">An obj contains Uid info and status of an Alumni.</param>
        /// <returns>204 status.</returns>
        /// <response code="204">Returns NoContent status</response>
        /// <response code="404">Returns Error status</response>
        [HttpPatch("activate")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ActiveAlumni([FromQuery] ActivateAlumniRequestBody requestBody)
        {
            try
            {
                await _alumniSvc.ActivateAlumniAsync(requestBody);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// [Admin] Endpoint for Admin Deactivation a alumni.
        /// </summary>
        /// <param name="id">ID of alumni</param>
        /// <returns>A Alumni within status 200 or 204 status.</returns>
        /// <response code="204">Returns 204 status when success</response>
        /// <response code="404">Returns NotFound status when Error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteAlumniAsync(int id)
        {
            try
            {
                await _alumniSvc.DeleteAlumniAsync(id);

                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }
        
        /// <summary>
        /// [Alumni] Alumni Request to Join Group
        /// </summary>
        /// <permission cref="RolesConstants.ALUMNI"></permission>
        /// <response code="200">Success Fully Joined A Group</response>
        /// <response code="400">Alumni Has been banned or already in group</response>
        /// <response code="403">Alumni Is Not Activated</response>
        /// <response code="404">Requested Group Not Found</response>
        [HttpPost("groups/{groupId:int}")]
        [Authorize(Roles = RolesConstants.ALUMNI)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinGroup(int groupId)
        {
            var value = User.FindFirst("id")?.Value;
            if (value != null)
            {
                var alumniId = int.Parse(value);
                await _alumniGroupSvc.JoinGroup(alumniId, groupId);
            }

            return Ok();
        }
        
        /// <summary>
        /// [Alumni] Alumni Cancel Request Join Group
        /// </summary>
        /// <permission cref="RolesConstants.ALUMNI"></permission>
        /// <response code="204">Success Cancel Request Leave A Group</response>
        /// <response code="400">Alumni has not been request in Group</response>
        /// <response code="403">User Is Not Activated or Token UserId not match with alumni</response>
        /// <response code="404">Requested alumni Not Found</response>
        [HttpDelete("groups/cancel/{groupId:int}")]
        [Authorize(Roles = RolesConstants.ALUMNI)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CancelRequestJoinGroup(int groupId)
        {
            var value = User.FindFirst("id")?.Value;
            if (value != null)
            {
                var alumniId = int.Parse(value);
                await _alumniGroupSvc.CancelRequestJoinGroup(alumniId, groupId);
            }

            return NoContent();
        }
        
        
        /// <summary>
        /// [Alumni] Alumni Leave Group
        /// </summary>
        /// <permission cref="RolesConstants.ALUMNI"></permission>
        /// <response code="204">Success Fully Leave A Group</response>
        /// <response code="400">Alumni Is Group Owner And There Are still Members in Group</response>
        /// <response code="403">User Is Not Activated or Token UserId not match with alumni</response>
        /// <response code="404">Requested alumni Not Found</response>
        [HttpDelete("groups/{groupId:int}")]
        [Authorize(Roles = RolesConstants.ALUMNI)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> LeaveGroup(int groupId)
        {
            var value = User.FindFirst("id")?.Value;
            if (value != null)
            {
                var alumniId = int.Parse(value);
                await _alumniGroupSvc.LeaveGroup(alumniId, groupId);
            }

            return NoContent();
        }
        
        /// <summary>
        /// [Alumni] Alumni Join Event
        /// </summary>
        /// <permission cref="RolesConstants.ALUMNI"></permission>
        /// <response code="200">Success Fully Joined A Event</response>
        /// <response code="400">Alumni Has been banned or already in Event</response>
        /// <response code="403">Alumni Is Not Activated</response>
        /// <response code="404">Requested Event Not Found</response>
        [HttpPost("event/{eventId:int}")]
        [Authorize(Roles = RolesConstants.ALUMNI)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinEvent(int eventId)
        {
            var value = User.FindFirst("id")?.Value;
            if (value != null)
            {
                var alumniId = int.Parse(value);
                await _eventRegistrationSvc.JoinEvent(alumniId, eventId);
            }

            return Ok();
        }
        
        /// <summary>
        /// [Alumni] User Leave Join Event
        /// </summary>
        /// <permission cref="RolesConstants.ALUMNI"></permission>
        /// <response code="204">Success Fully Leave A Event</response>
        /// <response code="400">Alumni Is still Members in Event</response>
        /// <response code="403">Alumni Is Not Activated or Token UserId not match with alumni</response>
        /// <response code="404">Requested alumni Not Found</response>
        [HttpDelete("event/{eventId:int}")]
        [Authorize(Roles = RolesConstants.ALUMNI)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CancelJoinEvent(int eventId)
        {
            var value = User.FindFirst("id")?.Value;
            if (value != null)
            {
                var alumniId = int.Parse(value);
                await _eventRegistrationSvc.LeaveEvent(alumniId, eventId);
            }

            return NoContent();
        }




    }


}