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
            SearchMajorModel searchMajorModel, int universityId);
        Task<MajorViewModel> GetMajorById(int id, int universityId);
        Task<MajorViewModel> CreateMajor(MajorCreateRequest request);
        Task<MajorViewModel> UpdateMajor(int id, MajorUpdateRequest request);
        Task DeleteMajor(int id);
    }
}
