using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.RecruitmentSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Recruitment;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/recruitments")]
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmetService;
        public RecruitmentController(IRecruitmentService service)
        {
            _recruitmetService = service;
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetRecruitments([FromQuery] SearchRecruitmentModel searchNewsModel, [FromQuery] PagingParam<RecruitmentEnum.RecruitmentSortCriteria> paginationModel)
        {
            var uniId = int.Parse(User.FindFirst("universityId")?.Value);
            var recruitments = _recruitmetService.GetRecruitments(paginationModel, searchNewsModel, uniId);
            return Ok(recruitments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetRecruitmentById(int id)
        {
            var uniId = int.Parse(User.FindFirst("universityId")?.Value);
            var recruitments = await _recruitmetService.GetRecruitmentById(id, uniId);
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = recruitments
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostGroup([FromBody] RecruitmentCreateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            var recruitmentModel = await _recruitmetService.CreateRecruitment(item, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "",
                Data = recruitmentModel
            });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] RecruitmentUpdateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            RecruitmentViewModel recruitmentModel = await _recruitmetService.UpdateRecruitment(id, item, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = recruitmentModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            await _recruitmetService.DeleteRecruitment(id, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "",
            });
        }
    }
}
