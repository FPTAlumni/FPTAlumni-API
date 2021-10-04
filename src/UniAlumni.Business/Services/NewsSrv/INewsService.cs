using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.News;

namespace UniAlumni.Business.Services.NewsSrv
{
    public interface INewsService
    {
        List<NewsViewModel> GetNews(PagingParam<NewsEnum.NewsSortCriteria> paginationModel,
            SearchNewsModel searchNewsModel, int universityId);
        Task<NewsDetailModel> GetNewsById(int id, int universityId);
        Task<NewsDetailModel> CreateNews(NewsCreateRequest request, int userId, bool isAdmin);
        Task<NewsDetailModel> UpdateNews(int id, NewsUpdateRequest request, int userId, bool isAdmin);
        Task DeleteNews(int id, int userId, bool isAdmin);
    }
}
