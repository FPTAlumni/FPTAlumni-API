using System;
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
using UniAlumni.DataTier.Repositories.ClassMajorRepo;
using UniAlumni.DataTier.Repositories.ClassRepo;
using UniAlumni.DataTier.Repositories.MajorRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Class;

namespace UniAlumni.Business.Services.ClassService
{
    public class ClassSvc : IClassSvc
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        private readonly IClassMajorRepository _classMajorRepository;
        private readonly IMajorRepository _majorRepository;

        public ClassSvc(IClassRepository classRepository, IMapper mapper, IClassMajorRepository classMajorRepository, IMajorRepository majorRepository)
        {
            _classRepository = classRepository;
            _mapper = mapper;
            _classMajorRepository = classMajorRepository;
            _majorRepository = majorRepository;
        }

        public IList<GetClassDetail> GetClassPage(PagingParam<ClassEnum.ClassSortCriteria> paginationModel,
            SearchClassModel searchClassModel)
        {
            IQueryable<Class> queryClass = _classRepository.Table
                .Include(c => c.University);

            if (searchClassModel.ClassOf is { Length: > 0 })
            {
                queryClass = queryClass.Where(c => c.ClassOf.Contains(searchClassModel.ClassOf));
            }

            // Apply Status
            queryClass = queryClass.Where(c => c.Status == (int)ClassEnum.ClassStatus.Active);

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
            Class classes = await _classRepository.Get(c => c.Id == id)
                .Include(c => c.University)
                .FirstOrDefaultAsync();
            if (classes == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Class not found");
            }
            if (classes.Status == (int)ClassEnum.ClassStatus.Inactive) return null;
            GetClassDetail classDetail = _mapper.Map<GetClassDetail>(classes);
            return classDetail;
        }

        public async Task<GetClassDetail> CreateClassAsync(CreateClassRequestBody requestBody)
        {
            Class classes = _mapper.Map<Class>(requestBody);
            classes.CreatedDate = DateTime.Now;
            classes.Status = (int)ClassEnum.ClassStatus.Active;
            await _classRepository.InsertAsync(classes);
            await _classRepository.SaveChangesAsync();

            GetClassDetail classDetail = _mapper.Map<GetClassDetail>(classes);
            return classDetail;
        }

        public async Task<GetClassDetail> UpdateClassAsync(UpdateClassRequestBody requestBody)
        {
            Class classes = await _classRepository.GetFirstOrDefaultAsync(alu => alu.Id == requestBody.Id);
            if (classes == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Class not found");
            }
            classes = _mapper.Map(requestBody, classes);
            classes.UpdatedDate = DateTime.Now;
            _classRepository.Update(classes);
            await _classRepository.SaveChangesAsync();
            GetClassDetail classDetail = _mapper.Map<GetClassDetail>(classes);
            return classDetail;
        }

        public async Task DeleteClassAsync(int id)
        {
            Class classes = await _classRepository.GetFirstOrDefaultAsync(alu => alu.Id == id);
            if (classes == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "Class not found");
            }
            classes.Status = (int)ClassEnum.ClassStatus.Inactive;
            await _classRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _classRepository.GetAll().CountAsync();
        }
        public async Task AddMajorToClass(int classId, ClassAddMajorsRequest request)
        {
            Class _class = await _classRepository.Get(c => c.Id == classId).Include(c => c.ClassMajors).FirstOrDefaultAsync();
            if (_class == null)
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching class");
            var classInactiveMajors = _class.ClassMajors.Where(cm => request.ListMajorId.Contains((int)cm.MajorId) && cm.Status != (byte)ClassMajorEnum.ClassMajorStatus.Active).ToList();
            var addingMajorId = request.ListMajorId.Where(_classId => _class.ClassMajors.All(cm => cm.MajorId != _classId)).ToList();
            foreach (var cm in classInactiveMajors)
            {
                cm.Status = (byte)ClassMajorEnum.ClassMajorStatus.Active;
            }
            List<ClassMajor> addingClassMajors = new List<ClassMajor>();

            foreach (var majorId in addingMajorId)
            {
                if (_majorRepository.Get(m => m.Id == majorId).FirstOrDefault() == null)
                    throw new MyHttpException(StatusCodes.Status400BadRequest, $"Cannot find major with id= {majorId}");
                addingClassMajors.Add(new ClassMajor { ClassId = classId, MajorId = majorId, Status = (byte)ClassMajorEnum.ClassMajorStatus.Active });
            }

            _classMajorRepository.ClassMajor.AddRange(addingClassMajors);
            await _classMajorRepository.SaveChangesAsync();
        }
        public async Task DeleteMajorToClass(int classId, int majorId)
        {
            Class _class = await _classRepository.Get(c => c.Id == classId).Include(c => c.ClassMajors).FirstOrDefaultAsync();
            if (_class == null)
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching class");
            var classMajor = _classMajorRepository.Get(cm => cm.ClassId == classId && cm.MajorId == majorId).FirstOrDefault();
            if (classMajor != null)
            {
                classMajor.Status = (byte)ClassMajorEnum.ClassMajorStatus.Inactive;
                _classMajorRepository.Update(classMajor);
                await _classMajorRepository.SaveChangesAsync();
            }
            else
            {
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching major in class to delete");
            }
        }
    }
}