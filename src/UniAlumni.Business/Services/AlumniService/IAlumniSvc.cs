using System.Collections.Generic;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.Business.Services.AlumniService
{
    /// <summary>
    /// Interface for service layer of Alumni in Business module.
    /// </summary>
    public interface IAlumniSvc
    {
        /// <summary>
        /// Get list of all Alumni.
        /// </summary>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <param name="searchAlumniModel">An object contains search and filter criteria</param>
        /// <returns>List of Users.</returns>
        IList<GetAlumniDetail> GetAlumniPage(PagingParam<AlumniEnum.AlumniSortCriteria> paginationModel,
            SearchAlumniModel searchAlumniModel);

        /// <summary>
        /// Get detail information of a Alumni.
        /// </summary>
        /// <param name="id">Id of Alumni.</param>
        /// <returns>A Alumni Detail.</returns>>
        public Task<GetAlumniDetail> GetAlumniProfile(int id);

        public Task<GetAlumniDetail> CreateAlumni(CreateAlumniRequestBody requestBody);

    }
}