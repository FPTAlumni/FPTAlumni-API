using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.UniversityService;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.University;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/university")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversitySvc _universitySvc;

        public UniversityController(IUniversitySvc universitySvc)
        {
            _universitySvc = universitySvc;
        }

        /// <summary>
        /// [Guest] Endpoint for get all university with condition
        /// </summary>
        /// <param name="searchUniversityModel">An object contains search and filter criteria</param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of alumni</returns>
        /// <response code="200">Returns the list of university</response>
        /// <response code="204">Returns if list of university is empty</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModelsResponse<UniversityViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUniversity([FromQuery] SearchUniversityModel searchUniversityModel,
            [FromQuery] PagingParam<UniversityEnum.UniversitySortCriteria> paginationModel)
        {
            IList<UniversityViewModel> result = _universitySvc.GetAllUniversity(paginationModel, searchUniversityModel);
            int total = await _universitySvc.GetTotal();
            
            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(new ModelsResponse<UniversityViewModel>()
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
        /// [Guest] Endpoint for get university by ID
        /// </summary>
        /// <param name="id">An id of university</param>
        /// <returns>List of university</returns>
        /// <response code="200">Returns the university</response>
        /// <response code="204">Returns if the university is not exist</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UniversityViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUniversityById(int id)
        {
            UniversityViewModel result = await _universitySvc.GetUniversityById(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(new BaseResponse<UniversityViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Data = result,
                Msg = ""
            });
        }
        
        /// <summary>
        /// [Admin] Endpoint for create pending university
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an university.</param>
        /// <returns>A university within status 201 or error status.</returns>
        /// <response code="201">Returns the university</response>
        /// <response code="404">Return if create is fail</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<UniversityViewModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUniversityAsync([FromBody] CreateUniversityRequestBody requestBody)
        {
            try
            {
                var result = await _universitySvc.CreateUniversityAsync(requestBody);

                return Created(string.Empty, new BaseResponse<UniversityViewModel>()
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
        /// [Admin] Endpoint for Admin edit university.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an university.</param>
        /// <returns>A university within status 200 or error status.</returns>
        /// <response code="200">Returns university after update</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(BaseResponse<UniversityViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUniversityAsync([FromBody] UpdateUniversityRequestBody requestBody)
        {
            try
            {
                UniversityViewModel updateUniversity = await _universitySvc.UpdateUniversityAsync(requestBody);

                return Ok(new BaseResponse<UniversityViewModel>()
                {
                    Code = StatusCodes.Status200OK,
                    Data = updateUniversity,
                    Msg = "Update Successful"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        /// <summary>
        /// [Admin] Endpoint for Admin Inactive a university.
        /// </summary>
        /// <param name="id">ID of university</param>
        /// <returns>A university within status 200 or 204 status.</returns>
        /// <response code="200">Returns 200 status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteUniversityAsync(int id)
        {
            try
            {
                await _universitySvc.DeleteUniversityAsync(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
        
        
        
        
        
        
        
        
    }
}