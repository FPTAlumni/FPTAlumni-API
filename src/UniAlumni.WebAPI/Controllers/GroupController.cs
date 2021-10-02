using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniAlumni.Business.Services.GroupSrv;
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
        public IActionResult GetPosts([FromQuery] SearchGroupModel searchGroupModel, [FromQuery] PagingParam<GroupEnum.GroupSortCriteria> paginationModel)
        {
            
            var groups = _groupService.GetGroups(paginationModel, searchGroupModel);
            return Ok(groups);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetPost(int id)
        {
            var group = await _groupService.GetGroupById(id);
            return Ok(group);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostGroup([FromBody] GroupCreateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            GroupViewModel groupModel = await _groupService.CreateGroup(item, userId, User.IsInRole(RolesConstants.ADMIN));
            return Created(string.Empty, groupModel);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupUpdateRequest item)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            GroupViewModel groupModel = await _groupService.UpdateGroup(id, item, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(groupModel);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            await _groupService.DeleteGroup(id, userId, User.IsInRole(RolesConstants.ADMIN));
            return NoContent();
        }
    }
}
