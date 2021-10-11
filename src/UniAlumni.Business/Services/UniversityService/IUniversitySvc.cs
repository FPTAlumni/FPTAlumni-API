using System.Collections.Generic;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.Business.Services.UniversityService
{
    /// <summary>
    /// Interface for service layer of University in Business module.
    /// </summary>
    public interface IUniversitySvc
    {
        /// <summary>
        /// Get list of all University.
        /// </summary>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <param name="searchUniversityModel">An object contains search criteria</param>
        /// <returns>List of University.</returns>
        IList<UniversityViewModel> GetAllUniversity(PagingParam<UniversityEnum.UniversitySortCriteria> paginationModel,
            SearchUniversityModel searchUniversityModel);
        
        /// <summary>
        /// Get detail information of a University.
        /// </summary>
        /// <param name="id">Id of University.</param>
        /// <returns>A University Detail.</returns>>
        public Task<UniversityViewModel> GetUniversityById(int id);

        /// <summary>
        /// Create University.
        /// </summary>
        /// <param name="requestBody">Model create University request of University.</param>
        /// <returns>A University Detail.</returns>>
        Task<UniversityViewModel> CreateUniversityAsync(CreateUniversityRequestBody requestBody);

        /// <summary>
        /// Update University.
        /// </summary>
        /// <param name="requestBody">Model Update University request of University.</param>
        /// <returns>A University Detail.</returns>>
        Task<UniversityViewModel> UpdateUniversityAsync(UpdateUniversityRequestBody requestBody);

        /// <summary>
        /// Delete University - Change Status to Deactivate
        /// </summary>
        /// <param name="id">ID of University</param>
        Task DeleteUniversityAsync(int id);
        
        /// <summary>
        /// Get total of University
        /// </summary>
        /// <returns>Total of University</returns>
        public Task<int> GetTotal();

    }
}