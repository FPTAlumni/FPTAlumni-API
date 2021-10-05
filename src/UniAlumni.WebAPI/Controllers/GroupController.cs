using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniAlumni.Business.Services.GroupSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/groups")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService service)
        {
            _groupService = service;
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetGroups([FromQuery] SearchGroupModel searchGroupModel, [FromQuery] PagingParam<GroupEnum.GroupSortCriteria> paginationModel)
        {
            var uniId = int.Parse(User.FindFirst("universityId")?.Value);
            var groups = _groupService.GetGroups(paginationModel, searchGroupModel, uniId);
            return Ok(groups);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetGroup(int id)
        {
            var uniId = int.Parse(User.FindFirst("universityId")?.Value);
            var group = await _groupService.GetGroupById(id, uniId);
            return Ok(new BaseResponse<GroupViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = group
            });
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostGroup([FromBody] GroupCreateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            GroupViewModel groupModel = await _groupService.CreateGroup(item, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<GroupViewModel>()
            {
                Code = StatusCodes.Status201Created,
                Msg = "",
                Data = groupModel
            });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupUpdateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            GroupViewModel groupModel = await _groupService.UpdateGroup(id, item, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<GroupViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = groupModel
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            await _groupService.DeleteGroup(id, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(new BaseResponse<GroupViewModel>()
            {
                Code = StatusCodes.Status204NoContent,
                Msg = "",
            });
        }
    }
}
