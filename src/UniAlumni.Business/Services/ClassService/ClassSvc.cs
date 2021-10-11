using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.ClassRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Class;

namespace UniAlumni.Business.Services.ClassService
{
    public class ClassSvc : IClassSvc
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassSvc(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public IList<GetClassDetail> GetClassPage(PagingParam<ClassEnum.ClassSortCriteria> paginationModel,
            SearchClassModel searchClassModel)
        {
            IQueryable<Class> queryClass = _classRepository.Table;

            if (searchClassModel.ClassOf is {Length: > 0})
            {
                queryClass = queryClass.Where(c => c.ClassOf.Contains(searchClassModel.ClassOf));
            }

            // Apply sort
            if (paginationModel.SortKey.ToString().Trim().Length > 0)
                queryClass =
                    queryClass.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            queryClass = queryClass.GetWithPaging(paginationModel.Page, paginationModel.PageSize).AsQueryable();

            IQueryable<GetClassDetail> classDetail = _mapper.ProjectTo<GetClassDetail>(queryClass);
            return classDetail.ToList();
        }

        public async Task<GetClassDetail> GetClassById(int id)
        {
            Class classes = await _classRepository.GetByIdAsync(id);
            GetClassDetail classDetail = _mapper.Map<GetClassDetail>(classes);
            return classDetail;
        }

        public async Task<GetClassDetail> CreateClassAsync(CreateClassRequestBody requestBody)
        {
            Class classes = _mapper.Map<Class>(requestBody);

            await _classRepository.InsertAsync(classes);
            await _classRepository.SaveChangesAsync();

            GetClassDetail classDetail = _mapper.Map<GetClassDetail>(classes);
            return classDetail;
        }

        public async Task<GetClassDetail> UpdateClassAsync(UpdateClassRequestBody requestBody)
        {
            Class classes = await _classRepository.GetFirstOrDefaultAsync(alu => alu.Id == requestBody.Id);
            classes = _mapper.Map(requestBody, classes);
            _classRepository.Update(classes);
            await _classRepository.SaveChangesAsync();
            GetClassDetail classDetail = _mapper.Map<GetClassDetail>(classes);
            return classDetail;
        }

        public async Task DeleteClassAsync(int id)
        {
            Class category = await _classRepository.GetFirstOrDefaultAsync(alu => alu.Id == id);
            _classRepository.Delete(category);
            await _classRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _classRepository.GetAll().CountAsync();
        }
    }
}