using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniAlumni.Business.Services.RecruitmentSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
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
            var userId = int.Parse(User.FindFirst("id")?.Value);
            var recruitments = _recruitmetService.GetRecruitments(paginationModel, searchNewsModel, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(recruitments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetRecruitmentById(int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            RecruitmentViewModel recruitment;
            try
            {
                recruitment = await _recruitmetService.GetRecruitmentById(id, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<RecruitmentViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
                Data = recruitment
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostRecruitment([FromBody] RecruitmentCreateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            RecruitmentViewModel recruitmentModel;
            try
            {
                recruitmentModel = await _recruitmetService.CreateRecruitment(item, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<RecruitmentViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "Created successfully",
                Data = recruitmentModel
            });
        }
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> UpdateRecruitment([FromBody] RecruitmentUpdateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            RecruitmentViewModel recruitmentModel;
            try
            {
                recruitmentModel = await _recruitmetService.UpdateRecruitment(item, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<RecruitmentViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Updated successfully",
                Data = recruitmentModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteRecruitment([FromRoute] int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            try
            {
                await _recruitmetService.DeleteRecruitment(id, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<RecruitmentViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<RecruitmentViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "Deleted successfully",
            });
        }
    }
}
