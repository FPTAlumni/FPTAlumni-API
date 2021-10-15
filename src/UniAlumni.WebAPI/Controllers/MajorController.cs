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
using UniAlumni.DataTier.Common.Exception;
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
        [AllowAnonymous]
        public IActionResult GetMajors([FromQuery] SearchMajorModel searchMajorModel, [FromQuery] PagingParam<MajorEnum.MajorSortCriteria> paginationModel)
        {
            var majors = _majorService.GetMajors(paginationModel, searchMajorModel, User.IsInRole(RolesConstants.ADMIN));
            return Ok(majors);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMajor(int id)
        {
            MajorViewModel major;
            try
            {
                major = await _majorService.GetMajorById(id, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<MajorViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<MajorViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
                Data = major
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> PostMajor([FromBody] MajorCreateRequest item)
        {
            MajorViewModel majorModel;
            try 
            {
                majorModel = await _majorService.CreateMajor(item);
            } 
            catch(MyHttpException e)
            {
                return Ok(new BaseResponse<MajorViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<MajorViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "Created successfully",
                Data = majorModel
            });
        }
        [HttpPut]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> UpdateMajor([FromBody] MajorUpdateRequest item)
        {
            MajorViewModel majorModel;
            try
            {
                majorModel = await _majorService.UpdateMajor(item);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<MajorViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<MajorViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "Created successfully",
                Data = majorModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteMajor([FromRoute] int id)
        {
            try
            {
                await _majorService.DeleteMajor(id);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<MajorViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<MajorViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "Deleted successfully",
            });
        }
    }
}
