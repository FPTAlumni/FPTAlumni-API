﻿using System.Collections.Generic;
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

    }
}