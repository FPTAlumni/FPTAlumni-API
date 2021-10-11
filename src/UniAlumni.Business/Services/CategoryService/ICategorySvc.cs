using System.Collections.Generic;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Category;

namespace UniAlumni.Business.Services.CategoryService
{
    /// <summary>
    /// Interface for service layer of Category in Business module.
    /// </summary>
    public interface ICategorySvc
    {
        /// <summary>
        /// Get list of all Category.
        /// </summary>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <param name="searchCategoryModel">An object contains search and filter criteria</param>
        /// <returns>List of Category.</returns>
        IList<GetCategoryDetail> GetCategoryPage(PagingParam<CategoryEnum.CategorySortCriteria> paginationModel,
            SearchCategoryModel searchCategoryModel);
        
        /// <summary>
        /// Get detail information of a Category.
        /// </summary>
        /// <param name="id">Id of Category.</param>
        /// <returns>A category Detail.</returns>>
        public Task<GetCategoryDetail> GetCategoryById(int id);

        /// <summary>
        /// Create Category.
        /// </summary>
        /// <param name="requestBody">Model create category request of Category.</param>
        /// <returns>A category detail.</returns>>
        public Task<GetCategoryDetail> CreateCategoryAsync(CreateCategoryRequestBody requestBody);

        /// <summary>
        /// Update category.
        /// </summary>
        /// <param name="requestBody">Model Update category request of Category.</param>
        /// <returns>A category Detail.</returns>>
        public Task<GetCategoryDetail> UpdateCategoryAsync(UpdateCategoryRequestBody requestBody);
        
        /// <summary>
        /// Delete category - Change Status to Inactive
        /// </summary>
        /// <param name="id">ID of Category</param>
        /// <returns></returns>
        public Task DeleteCategoryAsync(int id);
        
        /// <summary>
        /// Get total of Category
        /// </summary>
        /// <returns>Total of Category</returns>
        public Task<int> GetTotal();
    }
}