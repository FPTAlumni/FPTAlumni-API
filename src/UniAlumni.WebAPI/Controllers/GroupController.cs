using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UniAlumni.Business.Services.GroupService;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Request.Group;
using UniAlumni.DataTier.ViewModels;

namespace UniAlumni.WebAPI.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;
        public GroupController(GroupService service)
        {
            _groupService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery]PaginationModel paginationModel)
        {
            var groups = await _groupService.GetAllGroups(paginationModel);
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
            return Created($"/api/groups", groupModel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupUpdateRequest item, [FromQuery] int id, int userId, bool isAdmin)
        {
            GroupViewModel groupModel = await _groupService.UpdateGroup(id, item, userId, isAdmin);
            return Ok(groupModel);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            await _groupService.DeleteGroup(id);
            return NoContent();
        }
    }
}
