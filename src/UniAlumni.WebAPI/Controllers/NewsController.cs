using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.NewsSrv;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.News;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService service)
        {
            _newsService = service;
        }
        // GET: api/<NewsController>
        [HttpGet]
        //[Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetNews([FromQuery] SearchNewsModel searchNewsModel, [FromQuery] PagingParam<NewsEnum.NewsSortCriteria> paginationModel)
        {

            var news = _newsService.GetNews(paginationModel, searchNewsModel, 1);
            return Ok(news);
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        //[Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetPost(int id)
        {
            var news = await _newsService.GetNewsById(id, 1);
            return Ok(news);
        }

        // POST api/<NewsController>
        [HttpPost]
        //[Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> PostNews([FromBody] NewsCreateRequest item)
        {
            //var userId = int.Parse(User.FindFirst("id")?.Value);
            var newsDetail = await _newsService.CreateNews(item, 1, true);
            return Created(string.Empty, newsDetail);
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] NewsUpdateRequest item)
        {
            var newsDetail = await _newsService.UpdateNews(id, item, 1, true);
            return Ok(newsDetail);
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            //var userId = int.Parse(User.FindFirst("id")?.Value);
            await _newsService.DeleteNews(id, 1, true);
            return NoContent();
        }
    }
}
