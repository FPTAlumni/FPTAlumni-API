using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.Business.Services.MajorSrv
{
    public interface IMajorService
    {
        List<MajorViewModel> GetMajors(PagingParam<MajorEnum.MajorSortCriteria> paginationModel,
            SearchMajorModel searchMajorModel);
        Task<MajorViewModel> GetMajorById(int id);
        Task<MajorViewModel> CreateMajor(MajorCreateRequest request);
        Task<MajorViewModel> UpdateMajor(int id, MajorUpdateRequest request);
        Task DeleteMajor(int id);
    }
}
