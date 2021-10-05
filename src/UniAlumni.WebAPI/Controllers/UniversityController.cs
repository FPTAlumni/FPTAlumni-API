using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniAlumni.Business.Services.UniversityService;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
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
        /// <response code="200">Returns the list of alumni</response>
        /// <response code="204">Returns if list of alumni is empty</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IList<UniversityViewModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllUniversity([FromQuery] SearchUniversityModel searchUniversityModel,
            [FromQuery] PagingParam<UniversityEnum.UniversitySortCriteria> paginationModel)
        {
            IList<UniversityViewModel> result = _universitySvc.GetAllUniversity(paginationModel, searchUniversityModel);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
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
        [ProducesResponseType(typeof(UniversityViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUniversityById(int id)
        {
            UniversityViewModel result = await _universitySvc.GetUniversityById(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}