using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.EventService;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Event;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/event")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class EventController : ControllerBase
    {
        private readonly IEventSvc _eventSvc;

        public EventController(IEventSvc eventSvc)
        {
            _eventSvc = eventSvc;
        }
        
        
        /// <summary>
        /// [Admin,Alumni] Endpoint for get all event with condition
        /// </summary>
        /// <param name="searchEventModel"></param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of event</returns>
        /// <response code="200">Returns the list of event</response>
        /// <response code="204">Returns if list of event is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(ModelsResponse<GetEventDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEvent([FromQuery] SearchEventModel searchEventModel,
            [FromQuery] PagingParam<EventEnum.EventSortCriteria> paginationModel)
        {
            IList<GetEventDetail> result = await _eventSvc.GetEventPage(paginationModel, searchEventModel);
            int total = await _eventSvc.GetTotal();
            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(new ModelsResponse<GetEventDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result.ToList(),
                Metadata = new PagingMetadata()
                {
                    Page = paginationModel.Page,
                    Size = paginationModel.PageSize,
                    Total = total
                },
                Msg = ""
            });
        }


        /// <summary>
        /// [Admin,Alumni] Endpoint for get event by ID
        /// </summary>
        /// <param name="id">An id of event</param>
        /// <returns>List of event</returns>
        /// <response code="200">Returns the event</response>
        /// <response code="204">Returns if the event is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(BaseResponse<GetEventDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventById(int id)
        {
            GetEventDetail result = await _eventSvc.GetEventById(id);
            
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(new BaseResponse<GetEventDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result,
                Msg = ""
            });
        }

        /// <summary>
        /// [Admin] Endpoint for create event
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an event.</param>
        /// <returns>A event within status 201 or error status.</returns>
        /// <response code="201">Returns the event</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(BaseResponse<GetEventDetail>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventRequestBody requestBody)
        {
            var result = await _eventSvc.CreateEventAsync(requestBody);

            return Created(string.Empty, new BaseResponse<GetEventDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result,
                Msg = "Send Request Successful"
            });
        }

        /// <summary>
        /// [Admin] Endpoint for Admin edit event.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an event.</param>
        /// <returns>A event within status 200 or error status.</returns>
        /// <response code="200">Returns event after update</response>
        /// <response code="404">Returns Error</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(BaseResponse<GetEventDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEventAsync([FromBody] UpdateEventRequestBody requestBody)
        {
            try
            {
                GetEventDetail updateClass = await _eventSvc.UpdateEventAsync(requestBody);

                return Ok(new BaseResponse<GetEventDetail>()
                {
                    Code = StatusCodes.Status200OK,
                    Data = updateClass,
                    Msg = "Update Successful"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        /// <summary>
        /// [Admin] Endpoint for Admin Delete a event.
        /// </summary>
        /// <param name="id">ID of event</param>
        /// <returns>A event within status 200 or 204 status.</returns>
        /// <response code="404">Returns Error status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            try
            {
                await _eventSvc.DeleteEventAsync(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
    }
}