using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.ReferralSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Referral;

namespace UniAlumni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController : ControllerBase
    {
        private readonly IReferralService _referralService;
        public ReferralController(IReferralService service)
        {
            _referralService = service;
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetReferrals([FromQuery] SearchReferralModel searchReferralModel, [FromQuery] PagingParam<ReferralEnum.ReferralSortCriteria> paginationModel)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            var recruitments = _referralService.GetReferrals(paginationModel, searchReferralModel, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(recruitments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetReferralById(int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            var referral = await _referralService.GetReferralById(id, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = referral
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostReferral([FromBody] ReferralCreateRequest item)
        {
            var referralModel = await _referralService.CreateReferral(item);
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "",
                Data = referralModel
            });
        }
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> UpdateReferral([FromBody] ReferralUpdateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            var recruitmentModel = await _referralService.UpdateReferral(item);
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = recruitmentModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteReferral([FromRoute] int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            await _referralService.DeleteReferral(id, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "",
            });
        }
    }
}

