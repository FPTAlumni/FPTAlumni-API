using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.Business.Services.MajorSrv
{
    public interface IMajorService
    {
        ModelsResponse<MajorViewModel> GetMajors(PagingParam<MajorEnum.MajorSortCriteria> paginationModel,
            SearchMajorModel searchMajorModel, bool isAdmin);
        Task<MajorViewModel> GetMajorById(int id, bool isAdmin);
        Task<MajorViewModel> CreateMajor(MajorCreateRequest request);
        Task<MajorViewModel> UpdateMajor(MajorUpdateRequest request);
        Task DeleteMajor(int id);
    }
}
