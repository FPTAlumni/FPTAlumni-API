using System.Collections.Generic;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Company;

namespace UniAlumni.Business.Services.CompanyService
{
    public interface ICompanySvc
    {
        /// <summary>
        /// Get list of all Company.
        /// </summary>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <param name="searchCompanyModel">An object contains search and filter criteria</param>
        /// <returns>List of Company.</returns>
        IList<GetCompanyDetail> GetCompanyPage(PagingParam<CompanyEnum.CompanySortCriteria> paginationModel,
            SearchCompanyModel searchCompanyModel);
        
        /// <summary>
        /// Get detail information of a Company.
        /// </summary>
        /// <param name="id">Id of Company.</param>
        /// <returns>A Company Detail.</returns>>
        public Task<GetCompanyDetail> GetCompanyById(int id);

        /// <summary>
        /// Create Company.
        /// </summary>
        /// <param name="requestBody">Model create Company request of Company.</param>
        /// <returns>A Company detail.</returns>>
        public Task<GetCompanyDetail> CreateCompanyAsync(CreateCompanyRequestBody requestBody);
        
        /// <summary>
        /// Update Company.
        /// </summary>
        /// <param name="requestBody">Model Update Company request of Company.</param>
        /// <returns>A Company Detail.</returns>>
        public Task<GetCompanyDetail> UpdateCompanyAsync(UpdateCompanyRequestBody requestBody);
        
        /// <summary>
        /// Delete Company - Change Status to Inactive
        /// </summary>
        /// <param name="id">ID of Company</param>
        /// <returns></returns>
        public Task DeleteCompanyAsync(int id);
    }
}