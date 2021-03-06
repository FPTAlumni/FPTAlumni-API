using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.CategoryRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Category;

namespace UniAlumni.Business.Services.CategoryService
{
    public class CategorySvc : ICategorySvc
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategorySvc(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IList<GetCategoryDetail> GetCategoryPage(PagingParam<CategoryEnum.CategorySortCriteria> paginationModel,
            SearchCategoryModel searchCategoryModel)
        {
            IQueryable<Category> queryCategory = _categoryRepository.Table;

            if (searchCategoryModel.CategoryName is {Length: > 0})
                queryCategory = queryCategory.Where(c => c.CategoryName.Contains(searchCategoryModel.CategoryName));

            if (searchCategoryModel.Description is {Length: > 0})
                queryCategory = queryCategory.Where(c => c.Description.Contains(searchCategoryModel.Description));


            queryCategory = queryCategory.Where(c => c.Status == (byte?) CategoryEnum.CategoryStatus.Active);

            // Apply sort
            if (paginationModel.SortKey.ToString().Trim().Length > 0)
                queryCategory =
                    queryCategory.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            queryCategory = queryCategory.GetWithPaging(paginationModel.Page, paginationModel.PageSize).AsQueryable();

            IQueryable<GetCategoryDetail> categoryDetails = _mapper.ProjectTo<GetCategoryDetail>(queryCategory);
            return categoryDetails.ToList();
        }

        public async Task<GetCategoryDetail> GetCategoryById(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Category does not exist");
            }
            if (category.Status == (byte?) CategoryEnum.CategoryStatus.Inactive) return null;
            GetCategoryDetail categoryDetail = _mapper.Map<GetCategoryDetail>(category);
            return categoryDetail;
        }

        public async Task<GetCategoryDetail> CreateCategoryAsync(CreateCategoryRequestBody requestBody)
        {
            Category category = _mapper.Map<Category>(requestBody);
            category.Status = (byte?) CategoryEnum.CategoryStatus.Active;

            await _categoryRepository.InsertAsync(category);
            await _categoryRepository.SaveChangesAsync();

            GetCategoryDetail categoryDetail = _mapper.Map<GetCategoryDetail>(category);
            return categoryDetail;
        }

        public async Task<GetCategoryDetail> UpdateCategoryAsync(UpdateCategoryRequestBody requestBody)
        {
            Category category = await _categoryRepository.GetFirstOrDefaultAsync(alu => alu.Id == requestBody.Id);
            if (category == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Category does not exist");
            }
            category = _mapper.Map(requestBody, category);
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            GetCategoryDetail categoryDetail = _mapper.Map<GetCategoryDetail>(category);
            return categoryDetail;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category category = await _categoryRepository.GetFirstOrDefaultAsync(alu => alu.Id == id);
            if (category == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Category does not exist");
            }
            category.Status = (byte?) CategoryEnum.CategoryStatus.Inactive;
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _categoryRepository.GetAll().CountAsync();
        }
    }
}