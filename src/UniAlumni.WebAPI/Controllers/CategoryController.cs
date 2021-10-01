
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.CategoryService;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Category;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/category")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategorySvc _categorySvc;

        public CategoryController(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        /// <summary>
        /// [Admin,Alumni] Endpoint for get all category with condition
        /// </summary>
        /// <param name="searchCategoryModel"></param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of category</returns>
        /// <response code="200">Returns the list of category</response>
        /// <response code="204">Returns if list of category is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(IList<GetCategoryDetail>), StatusCodes.Status200OK)]
        public IActionResult GetAllCategory([FromQuery] SearchCategoryModel searchCategoryModel,
            [FromQuery] PagingParam<CategoryEnum.CategorySortCriteria> paginationModel)
        {
            IList<GetCategoryDetail> result = _categorySvc.GetCategoryPage(paginationModel, searchCategoryModel);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }


        /// <summary>
        /// [Admin,Alumni] Endpoint for get category by ID
        /// </summary>
        /// <param name="id">An id of category</param>
        /// <returns>List of category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="204">Returns if the category is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(GetCategoryDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            GetCategoryDetail result = await _categorySvc.GetCategoryById(id);
            
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }

        /// <summary>
        /// [Admin] Endpoint for create category
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an Category.</param>
        /// <returns>A category within status 201 or error status.</returns>
        /// <response code="201">Returns the category</response>
        /// <response code="204">Returns if the category is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(GetCategoryDetail), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestBody requestBody)
        {
            var result = await _categorySvc.CreateCategoryAsync(requestBody);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// [Admin] Endpoint for Admin edit category.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an category.</param>
        /// <returns>A category within status 200 or error status.</returns>
        /// <response code="200">Returns category after update</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(GetCategoryDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryRequestBody requestBody)
        {
            GetCategoryDetail updateAlumni = await _categorySvc.UpdateCategoryAsync(requestBody);

            return Ok(updateAlumni);
        }

        /// <summary>
        /// [Admin] Endpoint for Admin Inactive a category.
        /// </summary>
        /// <param name="id">ID of category</param>
        /// <returns>A category within status 200 or 204 status.</returns>
        /// <response code="200">Returns 200 status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                await _categorySvc.DeleteCategoryAsync(id);
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