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

        /// <summary>
        /// Create Alumni.
        /// </summary>
        /// <param name="requestBody">Model create alumni request of Alumni.</param>
        /// <returns>A Alumni Detail.</returns>>
        public Task<GetAlumniDetail> CreateAlumniAsync(CreateAlumniRequestBody requestBody);
        
        /// <summary>
        /// Update Alumni.
        /// </summary>
        /// <param name="requestBody">Model Update alumni request of Alumni.</param>
        /// <returns>A Alumni Detail.</returns>>
        public Task<GetAlumniDetail> UpdateAlumniAsync(UpdateAlumniRequestBody requestBody);

        
        /// <summary>
        /// Activate Alumni - Change Status to Active
        /// </summary>
        /// <param name="requestBody">Model Active Alumni</param>
        /// <returns></returns>
        public Task ActivateAlumniAsync(ActivateAlumniRequestBody requestBody);
        
        
        /// <summary>
        /// Delete Alumni - Change Status to Deactivate
        /// </summary>
        /// <param name="id">ID of Alumni</param>
        /// <returns></returns>
        public Task DeleteAlumniAsync(int id);
    }
}