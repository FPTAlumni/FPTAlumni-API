using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Recruitment;

namespace UniAlumni.Business.Services.RecruitmentSrv
{
    public interface IRecruitmentService
    {
        ModelsResponse<RecruitmentViewModel> GetRecruitments(PagingParam<RecruitmentEnum.RecruitmentSortCriteria> paginationModel,
            SearchRecruitmentModel searchRecruitmentModel, int userId, bool isAdmin);
        Task<RecruitmentViewModel> GetRecruitmentById(int id, int userId, bool isAdmin);
        Task<RecruitmentViewModel> CreateRecruitment(RecruitmentCreateRequest request, int userId, bool isAdmin);
        Task<RecruitmentViewModel> UpdateRecruitment(RecruitmentUpdateRequest request, int userId, bool isAdmin);
        Task<RecruitmentViewModel> UpdateRecruitmentStatus(RecruitmentUpdateStatusRequest request, bool isAdmin);
        Task<RecruitmentViewModel> UpdateRecruitmentEndDate(RecruitmentUpdateEndDateRequest request, int userId, bool isAdmin);
        Task DeleteRecruitment(int id, int userId, bool isAdmin);
    }
}
