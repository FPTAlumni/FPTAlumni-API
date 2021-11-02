using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.TagSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tags")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService service)
        {
            _tagService = service;
        }
        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetTags([FromQuery] PagingParam<TagEnum.TagSortCriteria> paginationModel)
        {
            try
            {
                var tags = _tagService.GetTags(paginationModel);
                return Ok(tags);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<TagViewModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
        }

    }
}
