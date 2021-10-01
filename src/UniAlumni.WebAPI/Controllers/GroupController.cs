using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniAlumni.Business.Services.GroupSrv;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
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
        public IActionResult GetPosts([FromQuery] SearchGroupModel searchGroupModel, [FromQuery] PagingParam<GroupEnum.GroupSortCriteria> paginationModel)
        {
            var groups = _groupService.GetGroups(paginationModel, searchGroupModel);
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var group = await _groupService.GetGroupById(id);
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupCreateRequest item, [FromQuery] int userId, bool isAdmin)
        {
            GroupViewModel groupModel = await _groupService.CreateGroup(item, userId, isAdmin);
            return Created(string.Empty, groupModel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupUpdateRequest item, [FromQuery] int userId, bool isAdmin)
        {
            GroupViewModel groupModel = await _groupService.UpdateGroup(id, item, userId, isAdmin);
            return Ok(groupModel);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id, [FromQuery] int userId, bool isAdmin)
        {
            await _groupService.DeleteGroup(id, userId, isAdmin);
            return NoContent();
        }
    }
}
