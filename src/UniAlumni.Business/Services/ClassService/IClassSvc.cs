using System.Collections.Generic;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Class;

namespace UniAlumni.Business.Services.ClassService
{
    /// <summary>
    /// Interface for service layer of Class in Business module.
    /// </summary>
    public interface IClassSvc
    {
        /// <summary>
        /// Get list of all Class.
        /// </summary>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <param name="searchClassModel">An object contains search and filter criteria</param>
        /// <returns>List of Class.</returns>
        IList<GetClassDetail> GetClassPage(PagingParam<ClassEnum.ClassSortCriteria> paginationModel,
            SearchClassModel searchClassModel);
        
        /// <summary>
        /// Get detail information of a Class.
        /// </summary>
        /// <param name="id">Id of Class.</param>
        /// <returns>A Class Detail.</returns>>
        public Task<GetClassDetail> GetClassById(int id);
        
        /// <summary>
        /// Create Class.
        /// </summary>
        /// <param name="requestBody">Model create Class request of Class.</param>
        /// <returns>A Class detail.</returns>>
        public Task<GetClassDetail> CreateClassAsync(CreateClassRequestBody requestBody);

        /// <summary>
        /// Update Class.
        /// </summary>
        /// <param name="requestBody">Model Update Class request of Class.</param>
        /// <returns>A Class Detail.</returns>>
        public Task<GetClassDetail> UpdateClassAsync(UpdateClassRequestBody requestBody);
        
        /// <summary>
        /// Delete Class - Change Status to Inactive
        /// </summary>
        /// <param name="id">ID of Class</param>
        /// <returns></returns>
        public Task DeleteClassAsync(int id);
        
        /// <summary>
        /// Get total of Class
        /// </summary>
        /// <returns>Total of Class</returns>
        public Task<int> GetTotal();
    }
}