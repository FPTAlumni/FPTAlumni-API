using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
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

        public NewsService(INewsRepository newsRepository, ITagNewsRepository tagNewsRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _mapper = mapper.ConfigurationProvider;
            _newsRepository = newsRepository;
            _tagNewsRepository = tagNewsRepository;
            _tagRepository = tagRepository;
        }

        public async Task<NewsDetailModel> CreateNews(NewsCreateRequest request, int userId, bool isAdmin)
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
                    tag = new Tag() { Tagname = tagName, Status= (byte)TagEnum.TagStatus.Active };
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

        public async Task DeleteNews(int id, int userId, bool isAdmin)
        {
            var news = await _newsRepository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (news != null)
            {
                news.Status = (int)NewsEnum.NewsStatus.Inactive;
                news.UpdatedDate = DateTime.Now;
                _newsRepository.Update(news);
                await _newsRepository.SaveChangesAsync();
            }
        }

        public List<NewsViewModel> GetNews(PagingParam<NewsEnum.NewsSortCriteria> paginationModel, SearchNewsModel searchNewsModel, int universityId)
        {
            var queryNews = _newsRepository.Get(p => p.Group.UniversityMajor.UniversityId == universityId);
            if (searchNewsModel.GroupId != null)
                queryNews = queryNews.Where(p => p.GroupId == searchNewsModel.GroupId);
            if (searchNewsModel.CategoryId != null)
                queryNews = queryNews.Where(p => p.CategoryId == searchNewsModel.CategoryId);
            if (searchNewsModel.TagId != null)
                queryNews = queryNews.Where(p => p.TagNews.Any(tn => tn.TagId == searchNewsModel.TagId));
            queryNews = queryNews.Where(p => p.Status == (byte?)searchNewsModel.Status);
            var newsViewModels = queryNews.ProjectTo<NewsViewModel>(_mapper);
            newsViewModels = newsViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            return newsViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();
        }

        public async Task<NewsDetailModel> GetNewsById(int id, int universityId)
        {
            var newsDetail = await _newsRepository.Get(p => p.Id == id && p.Group.UniversityMajor.UniversityId==universityId)
                .ProjectTo<NewsDetailModel>(_mapper).FirstOrDefaultAsync();
            return newsDetail;
        }

        public async Task<NewsDetailModel> UpdateNews(int id, NewsUpdateRequest request, int userId, bool isAdmin)
        {
            var news = await _newsRepository.Get(n => n.Id == id)
                .Include(n => n.TagNews)
                .ThenInclude(tn => tn.Tag)
                .FirstOrDefaultAsync();
            var mapper = _mapper.CreateMapper();
            news = mapper.Map(request, news);
            news.UpdatedDate = DateTime.Now;
            var tagNames = request.TagNames.Distinct();
            var newsTagNews = _tagNewsRepository.Get(p => p.NewsId == id);
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
