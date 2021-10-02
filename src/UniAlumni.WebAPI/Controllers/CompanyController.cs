using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.CompanyService;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Company;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/company")]
    [Consumes(MediaType.ApplicationJson)]
    [Produces(MediaType.ApplicationJson)]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanySvc _companySvc;

        public CompanyController(ICompanySvc companySvc)
        {
            _companySvc = companySvc;
        }

        /// <summary>
        /// [Admin,Alumni] Endpoint for get all Company with condition
        /// </summary>
        /// <param name="searchCompanyModel"></param>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <returns>List of Company</returns>
        /// <response code="200">Returns the list of Company</response>
        /// <response code="204">Returns if list of Company is empty</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(IList<GetCompanyDetail>), StatusCodes.Status200OK)]
        public IActionResult GetAllCompany([FromQuery] SearchCompanyModel searchCompanyModel,
            [FromQuery] PagingParam<CompanyEnum.CompanySortCriteria> paginationModel)
        {
            IList<GetCompanyDetail> result = _companySvc.GetCompanyPage(paginationModel, searchCompanyModel);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }


        /// <summary>
        /// [Admin,Alumni] Endpoint for get Company by ID
        /// </summary>
        /// <param name="id">An id of Company</param>
        /// <returns>List of Company</returns>
        /// <response code="200">Returns the Company</response>
        /// <response code="204">Returns if the Company is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        [ProducesResponseType(typeof(GetCompanyDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            GetCompanyDetail result = await _companySvc.GetCompanyById(id);
            
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }

        /// <summary>
        /// [Admin] Endpoint for create Company
        /// </summary>
        /// <param name="requestBody">An obj contains input info of an Company.</param>
        /// <returns>A Company within status 201 or error status.</returns>
        /// <response code="201">Returns the Company</response>
        /// <response code="204">Returns if the Company is not exist</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(GetCompanyDetail), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequestBody requestBody)
        {
            var result = await _companySvc.CreateCompanyAsync(requestBody);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// [Admin] Endpoint for Admin edit Company.
        /// </summary>
        /// <param name="requestBody">An obj contains update info of an Company.</param>
        /// <returns>A Company within status 200 or error status.</returns>
        /// <response code="200">Returns Company after update</response>
        /// <response code="403">Return if token is access denied</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(typeof(GetCompanyDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompanyAsync([FromBody] UpdateCompanyRequestBody requestBody)
        {
            GetCompanyDetail updateAlumni = await _companySvc.UpdateCompanyAsync(requestBody);

            return Ok(updateAlumni);
        }

        /// <summary>
        /// [Admin] Endpoint for Admin Inactive a Company.
        /// </summary>
        /// <param name="id">ID of Company</param>
        /// <returns>A Company within status 200 or 204 status.</returns>
        /// <response code="200">Returns 200 status</response>
        /// <response code="204">Returns NoContent status</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            try
            {
                await _companySvc.DeleteCompanyAsync(id);
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