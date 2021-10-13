using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.VoucherSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Voucher;

namespace UniAlumni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService service)
        {
            _voucherService = service;
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetVouchers([FromQuery] SearchVoucherModel searchVoucherModel, [FromQuery] PagingParam<VoucherEnum.VoucherSortCriteria> paginationModel)
        {
            var vouchers = _voucherService.GetVouchers(paginationModel, searchVoucherModel);
            return Ok(vouchers);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetVoucher(int id)
        {
            var voucher = await _voucherService.GetVoucherById(id);
            return Ok(new BaseResponse<VoucherViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = voucher
            });
        }
        

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> PostVoucher([FromBody] VoucherCreateRequest item)
        {
            VoucherViewModel voucherModel = await _voucherService.CreateVoucher(item);
            return Ok(new BaseResponse<VoucherViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "",
                Data = voucherModel
            });
        }
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> UpdateVoucher([FromBody] VoucherUpdateRequest item)
        {
            VoucherViewModel voucherModel = await _voucherService.UpdateVoucher(item);
            return Ok(new BaseResponse<VoucherViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = voucherModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            await _voucherService.DeleteVoucher(id);
            return Ok(new BaseResponse<VoucherViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "",
            });
        }
    }
}

