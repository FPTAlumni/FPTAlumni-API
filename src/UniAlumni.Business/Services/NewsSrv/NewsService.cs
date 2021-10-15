using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
using UniAlumni.DataTier.Repositories.NewsRepo;
using UniAlumni.DataTier.Repositories.TagNewsRepo;
using UniAlumni.DataTier.Repositories.TagRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.News;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.Business.Services.NewsSrv
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly ITagNewsRepository _tagNewsRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IConfigurationProvider _mapper;
        private readonly IAlumniGroupRepository _alumniGroupRepository;

        public NewsService(INewsRepository newsRepository, ITagNewsRepository tagNewsRepository, ITagRepository tagRepository, IMapper mapper,
            IAlumniGroupRepository alumniGroupRepository)
        {
            _mapper = mapper.ConfigurationProvider;
            _newsRepository = newsRepository;
            _tagNewsRepository = tagNewsRepository;
            _tagRepository = tagRepository;
            _alumniGroupRepository = alumniGroupRepository;
        }

        public async Task<NewsDetailModel> CreateNews(NewsCreateRequest request)
        {
            var mapper = _mapper.CreateMapper();
            var news = mapper.Map<News>(request);
            news.Status = (byte)NewsEnum.NewsStatus.Active;
            var tagNames = request.TagNames.Distinct();
            foreach (string tagName in tagNames)
            {
                TagNews tagNews = new TagNews();
                var tag = await _tagRepository.GetFirstOrDefaultAsync(t => t.Tagname.Equals(tagName));
                if (tag == null)
                {
                    tag = new Tag() { Tagname = tagName, Status = (byte)TagEnum.TagStatus.Active };
                    tagNews.Tag = tag;
                    news.TagNews.Add(tagNews);
                }
                else
                {
                    if (tag.Status != (byte)TagEnum.TagStatus.Banned)
                    {
                        if (tag.Status != (byte)TagEnum.TagStatus.Active)
                            tag.Status = (byte)TagEnum.TagStatus.Active;
                        tagNews.Tag = tag;
                        news.TagNews.Add(tagNews);
                    }
                }

            }
            _newsRepository.Insert(news);
            await _newsRepository.SaveChangesAsync();
            return await _newsRepository.Get(p => p.Id == news.Id).ProjectTo<NewsDetailModel>(_mapper).FirstOrDefaultAsync();
        }

        public async Task DeleteNews(int id)
        {
            var news = await _newsRepository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (news == null)
            {
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching news");
            }
            news.Status = (int)NewsEnum.NewsStatus.Inactive;
            news.UpdatedDate = DateTime.Now;
            _newsRepository.Update(news);
            await _newsRepository.SaveChangesAsync();
        }

        public ModelsResponse<NewsDetailModel> GetNews(PagingParam<NewsEnum.NewsSortCriteria> paginationModel, SearchNewsModel searchNewsModel, int userId, bool isAdmin)
        {
            var queryNews = _newsRepository.GetAll();
            if (!isAdmin)
            {
                var alumniGroupIds = _alumniGroupRepository.Get(ag => ag.AlumniId == userId).Select(ag => ag.GroupId);
                queryNews = queryNews.Where(n => alumniGroupIds.Contains((int)n.GroupId) && n.Status == (byte)NewsEnum.NewsStatus.Active);
            }
            else if (searchNewsModel.Status != null)
            {
                queryNews = queryNews.Where(n => n.Status == (byte?)searchNewsModel.Status);
            }
            if (searchNewsModel.GroupId != null)
                queryNews = queryNews.Where(n => n.GroupId == searchNewsModel.GroupId);
            if (searchNewsModel.CategoryId != null)
                queryNews = queryNews.Where(n => n.CategoryId == searchNewsModel.CategoryId);
            if (searchNewsModel.TagId != null)
                queryNews = queryNews.Where(n => n.TagNews.Any(tn => tn.TagId == searchNewsModel.TagId));

            var newsViewModels = queryNews.ProjectTo<NewsDetailModel>(_mapper);
            newsViewModels = newsViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            var data = newsViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<NewsDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "Retrieved successfully",
                Data = data,
                Metadata = new PagingMetadata()
                {
                    Page = paginationModel.Page,
                    Size = paginationModel.PageSize,
                    Total = data.Count
                }
            };
        }

        public async Task<NewsDetailModel> GetNewsById(int id, int userId, bool isAdmin)
        {
            var newsQuery = _newsRepository.Get(n => n.Id == id);
            if (!isAdmin)
            {
                var alumniGroupIds = _alumniGroupRepository.Get(ag => ag.AlumniId == userId).Select(ag => ag.GroupId);
                newsQuery = newsQuery.Where(n => alumniGroupIds.Contains((int)n.GroupId) && n.Status == (byte)NewsEnum.NewsStatus.Active);
            }
            if (newsQuery == null || !newsQuery.Any())
                throw new MyHttpException(StatusCodes.Status404NotFound, "Cannot find matching news");
            return await newsQuery.ProjectTo<NewsDetailModel>(_mapper).FirstOrDefaultAsync();
        }

        public async Task<NewsDetailModel> UpdateNews(NewsUpdateRequest request)
        {
            var news = await _newsRepository.Get(n => n.Id == request.Id)
                .Include(n => n.TagNews)
                .ThenInclude(tn => tn.Tag)
                .FirstOrDefaultAsync();
            if (news == null)
            {
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching news");
            }
            var mapper = _mapper.CreateMapper();
            news = mapper.Map(request, news);
            news.UpdatedDate = DateTime.Now;
            var tagNames = request.TagNames.Distinct();
            var newsTagNews = _tagNewsRepository.Get(p => p.NewsId == request.Id);
            var removingTagNews = newsTagNews.Where(tn => tagNames.All(r => r != tn.Tag.Tagname));
            _tagNewsRepository.TagNews.RemoveRange(removingTagNews);

            foreach (string tagName in tagNames)
            {
                TagNews tagNews = new TagNews() { NewsId = news.Id };
                var tag = await _tagRepository.GetFirstOrDefaultAsync(t => t.Tagname.Equals(tagName));
                if (tag == null)
                {
                    tag = new Tag() { Tagname = tagName, Status = (byte)TagEnum.TagStatus.Active };
                    tagNews.Tag = tag;
                    news.TagNews.Add(tagNews);
                }
                else
                {
                    if (tag.Status != (byte)TagEnum.TagStatus.Banned)
                    {
                        if (tag.Status != (byte)TagEnum.TagStatus.Active)
                            tag.Status = (byte)TagEnum.TagStatus.Active;
                        tagNews.TagId = tag.Id;
                        if (news.TagNews.All(tn => tn.TagId != tagNews.TagId))
                            news.TagNews.Add(tagNews);
                    }
                }
            }
            _newsRepository.Update(news);
            await _newsRepository.SaveChangesAsync();
            return await _newsRepository.Get(p => p.Id == news.Id).ProjectTo<NewsDetailModel>(_mapper).FirstOrDefaultAsync();
        }
    }
}
