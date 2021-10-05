using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.MajorSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Major;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/majors")]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _majorService;
        public MajorController(IMajorService service)
        {
            _majorService = service;
        }
        
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetMajors([FromQuery] SearchMajorModel searchMajorModel, [FromQuery] PagingParam<MajorEnum.MajorSortCriteria> paginationModel)
        {
            var uniId = int.Parse(User.FindFirst("universityId")?.Value);
            var majors = _majorService.GetMajors(paginationModel, searchMajorModel, uniId);
            return Ok(majors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetMajor(int id)
        {
            var uniId = int.Parse(User.FindFirst("universityId")?.Value);
            var major = await _majorService.GetMajorById(id, uniId);
            return Ok(new BaseResponse<MajorViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = major
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> PostMajor([FromBody] MajorCreateRequest item)
        {
            MajorViewModel majorModel = await _majorService.CreateMajor(item);
            return Created(string.Empty, majorModel);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> UpdateMajor(int id, [FromBody] MajorUpdateRequest item)
        {
            MajorViewModel majorModel = await _majorService.UpdateMajor(id, item);
            return Ok(majorModel);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteMajor([FromRoute] int id)
        {
            await _majorService.DeleteMajor(id);
            return NoContent();
        }
    }
}
