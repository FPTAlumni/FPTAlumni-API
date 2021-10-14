using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.Business.Services.NewsSrv;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Object;
using UniAlumni.DataTier.ViewModels.News;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniAlumni.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService service)
        {
            _newsService = service;
        }

        [HttpGet]
        [Route("news")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public IActionResult GetNews([FromQuery] SearchNewsModel searchNewsModel, [FromQuery] PagingParam<NewsEnum.NewsSortCriteria> paginationModel)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            var news = _newsService.GetNews(paginationModel, searchNewsModel, userId, User.IsInRole(RolesConstants.ADMIN));
            return Ok(news);
        }

        // GET api/<NewsController>/5
        [HttpGet("news/{id}")]
        [Authorize(Roles = RolesConstants.ADMIN_ALUMNI)]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var userId = int.Parse(User.FindFirst("id")?.Value);
            NewsDetailModel news;
            try
            {
                news = await _newsService.GetNewsById(id, userId, User.IsInRole(RolesConstants.ADMIN));
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<NewsDetailModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<NewsDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
                Data = news
            });
        }

        // POST api/<NewsController>
        [HttpPost("news")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> PostNews([FromBody] NewsCreateRequest item)
        {
            //var userId = int.Parse(User.FindFirst("id")?.Value);
            NewsDetailModel newsDetail;
            try
            {
                newsDetail = await _newsService.CreateNews(item);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<NewsDetailModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<NewsDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Created successfully",
                Data = newsDetail
            }); ;
        }

        // PUT api/<NewsController>/5
        [HttpPut("news")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> UpdateNews([FromBody] NewsUpdateRequest item)
        {
            NewsDetailModel newsDetail;
            try
            {
                newsDetail = await _newsService.UpdateNews(item);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<NewsDetailModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<NewsDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Updated successfully",
                Data = newsDetail
            });
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("news/{id}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteNews([FromRoute] int id)
        {
            try
            {
                await _newsService.DeleteNews(id);
            }
            catch (MyHttpException e)
            {
                return Ok(new BaseResponse<NewsDetailModel>
                {
                    Code = e.errorCode,
                    Msg = e.Message
                });
            }
            return Ok(new BaseResponse<NewsDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Updated successfully",
            });
        }
    }
}
