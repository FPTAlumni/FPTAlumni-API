using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using UniAlumni.Business.Services.PushNotification;
using UniAlumni.Business.Services.ReferralSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.Referral;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
            var referrals = _referralService.GetReferrals(paginationModel, searchReferralModel, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(referrals);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetReferralById(int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            ReferralViewModel referral;
            try
            {
                referral = await _referralService.GetReferralById(id, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<ReferralViewModel>()
                {
                    Code = e.errorCode,
                    Msg = e.Message,
                });
            }
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
                Data = referral
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostReferral([FromBody] ReferralCreateRequest item)
        {
            ReferralViewModel referralModel;
            try
            {
                referralModel = await _referralService.CreateReferral(item);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<ReferralViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "Created successfully",
                Data = referralModel
            });
        }
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> UpdateReferral([FromBody] ReferralUpdateRequest item)
        {
            ReferralViewModel referralModel;
            var userId = int.Parse(User.FindFirst("id")?.Value);
            try
            {
                referralModel = await _referralService.UpdateReferral(item, User.IsInRole(RolesConstants.ADMIN), userId);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<ReferralViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Updated successfully",
                Data = referralModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteReferral([FromRoute] int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            try
            {
                await _referralService.DeleteReferral(id, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<ReferralViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<ReferralViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "Deleted successfully",
            });
        }
        
        /// <summary>
        /// [Admin] Endpoint to approve for Referral.
        /// </summary>
        /// <param name="requestBody">An obj contains Id and status of an Referral.</param>
        /// <returns>204 status.</returns>
        /// <response code="204">Returns NoContent status</response>
        /// <response code="403">Returns Forbidden </response>
        /// <response code="404">Returns Error status</response>
        [HttpPatch("approve")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ApproveReferral([FromQuery] ApproveReferralRequestBody requestBody)
        {
            try
            {
                Referral referral = await _referralService.ApproveReferral(requestBody);
                String message = "";
                if (referral.Status != null)
                {
                    message = referral.FullName + " has been " +
                                     Enum.GetNames<ReferralEnum.ReferralStatus>()[(int) referral.Status];
                }
                
                await PushNotification
                    .SendMessage(referral.Nominator.Uid, "Referral Status", message);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

