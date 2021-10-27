using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.ClassService;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Class;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/class")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class ClassController : ControllerBase
    {
        private readonly IClassSvc _classSvc;

        public ClassController(IClassSvc classSvc)
        {
            _classSvc = classSvc;
        }
        
        /// <summary>
        /// [Guest] Endpoint for get all class with condition
        /// </summary>
        /// <param name="searchClassModel"></param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of class</returns>
        /// <response code="200">Returns the list of class</response>
        /// <response code="204">Returns if list of class is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModelsResponse<GetClassDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClass([FromQuery] SearchClassModel searchClassModel,
            [FromQuery] PagingParam<ClassEnum.ClassSortCriteria> paginationModel)
        {
            IList<GetClassDetail> result = _classSvc.GetClassPage(paginationModel, searchClassModel);
            int total = await _classSvc.GetTotal();
            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(new ModelsResponse<GetClassDetail>()
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
        /// [Guest] Endpoint for get class by ID
        /// </summary>
        /// <param name="id">An id of class</param>
        /// <returns>List of class</returns>
        /// <response code="200">Returns the class</response>
        /// <response code="204">Returns if the class is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<GetClassDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClassById(int id)
        {
            GetClassDetail result = await _classSvc.GetClassById(id);
            
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(new BaseResponse<GetClassDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result,
                Msg = ""
            });
        }

        /// <summary>
        /// [Admin] Endpoint for create class
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an class.</param>
        /// <returns>A class within status 201 or error status.</returns>
        /// <response code="201">Returns the class</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(BaseResponse<GetClassDetail>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassRequestBody requestBody)
        {
            var result = await _classSvc.CreateClassAsync(requestBody);

            return Created(string.Empty, new BaseResponse<GetClassDetail>()
            {
                Code = StatusCodes.Status200OK,
                Data = result,
                Msg = "Send Request Successful"
            });
        }

        /// <summary>
        /// [Admin] Endpoint for Admin edit class.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an class.</param>
        /// <returns>A class within status 200 or error status.</returns>
        /// <response code="200">Returns class after update</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(BaseResponse<GetClassDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateClassAsync([FromBody] UpdateClassRequestBody requestBody)
        {
            try
            {
                GetClassDetail updateClass = await _classSvc.UpdateClassAsync(requestBody);

                return Ok(new BaseResponse<GetClassDetail>()
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

        [HttpPut("{id}/majors")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> AddMajorsToClass(int id, [FromBody] ClassAddMajorsRequest request)
        {
            try
            {
                await _classSvc.AddMajorToClass(id, request);

                return Ok(new BaseResponse<GetClassDetail>()
                {
                    Code = StatusCodes.Status200OK,
                    Msg = "Update Successful"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        /// [Admin] Endpoint for Admin Delete a class.
        /// </summary>
        /// <param name="id">ID of class</param>
        /// <returns>A class within status 200 or 204 status.</returns>
        /// <response code="200">Returns 200 status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteClassAsync(int id)
        {
            try
            {
                await _classSvc.DeleteClassAsync(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
    }
}