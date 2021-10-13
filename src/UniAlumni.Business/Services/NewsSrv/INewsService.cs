using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.News;

namespace UniAlumni.Business.Services.NewsSrv
{
    public interface INewsService
    {
        ModelsResponse<NewsViewModel> GetNews(PagingParam<NewsEnum.NewsSortCriteria> paginationModel, SearchNewsModel searchNewsModel, int userId, bool isAdmin);
        Task<NewsDetailModel> GetNewsById(int id, int userId, bool isAdmin);
        Task<NewsDetailModel> CreateNews(NewsCreateRequest request);
        Task<NewsDetailModel> UpdateNews(NewsUpdateRequest request);
        Task DeleteNews(int id);
    }
}
