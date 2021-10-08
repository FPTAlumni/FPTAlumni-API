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
        ModelsResponse<NewsViewModel> GetNews(PagingParam<NewsEnum.NewsSortCriteria> paginationModel,
            SearchNewsModel searchNewsModel, int universityId);
        ModelsResponse<NewsViewModel> GetNewsByGroupId(PagingParam<NewsEnum.NewsSortCriteria> paginationModel, UserSearchNewsModel searchNewsModel,
                                                       int universityId, int groupId);
        Task<NewsDetailModel> GetNewsById(int id, int universityId);
        Task<NewsDetailModel> CreateNews(NewsCreateRequest request, int userId, bool isAdmin);
        Task<NewsDetailModel> UpdateNews(NewsUpdateRequest request, int userId, bool isAdmin);
        Task DeleteNews(int id, int userId, bool isAdmin);
    }
}
