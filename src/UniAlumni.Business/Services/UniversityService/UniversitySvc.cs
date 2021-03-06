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
using UniAlumni.DataTier.Repositories.UniversityRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.Business.Services.UniversityService
{
    public class UniversitySvc : IUniversitySvc
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public UniversitySvc(IUniversityRepository universityRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        public IList<UniversityViewModel> GetAllUniversity(
            PagingParam<UniversityEnum.UniversitySortCriteria> paginationModel,
            SearchUniversityModel searchUniversityModel)
        {
            IQueryable<University> queryUniversity = _universityRepository.Table
                .Include(u=>u.Classes);
            if (searchUniversityModel.Name is {Length : > 0})
            {
                queryUniversity =
                    queryUniversity.Where(university => university.Name.Contains(searchUniversityModel.Name));
            }

            //Apply Status
            queryUniversity = queryUniversity.Where(u => u.Status == (byte?) UniversityEnum.UniversityStatus.Active);
            // Apply Sort
            if (paginationModel.SortKey.ToString().Trim().Length > 0)
                queryUniversity =
                    queryUniversity.GetWithSorting(paginationModel.SortKey.ToString(),
                        paginationModel.SortOrder);
            // Apply Paging
            queryUniversity = queryUniversity.GetWithPaging(paginationModel.Page, paginationModel.PageSize)
                .AsQueryable();
            // Mapping
            IQueryable<UniversityViewModel>
                queryUniversityDto = _mapper.ProjectTo<UniversityViewModel>(queryUniversity);

            return queryUniversityDto.ToList();
        }

        public async Task<UniversityViewModel> GetUniversityById(int id)
        {
            
            University university = await _universityRepository.Get(u=>u.Id == id)
                .Include(u=>u.Classes)
                .FirstOrDefaultAsync();
            if (university == null) throw new MyHttpException(StatusCodes.Status404NotFound, "Event not found");

            if(university.Status == (byte?) UniversityEnum.UniversityStatus.Inactive) return null;
            UniversityViewModel universityDetail = _mapper.Map<UniversityViewModel>(university);
            return universityDetail;
        }

        public async Task<UniversityViewModel> CreateUniversityAsync(CreateUniversityRequestBody requestBody)
        {
            University university = _mapper.Map<University>(requestBody);
            university.CreatedDate = DateTime.Now;
            university.Status = (byte?) UniversityEnum.UniversityStatus.Active;
            await _universityRepository.InsertAsync(university);
            await _universityRepository.SaveChangesAsync();

            UniversityViewModel universityDetail = _mapper.Map<UniversityViewModel>(university);
            return universityDetail;
        }

        public async Task<UniversityViewModel> UpdateUniversityAsync(UpdateUniversityRequestBody requestBody)
        {
            University university = await _universityRepository.GetFirstOrDefaultAsync(alu => alu.Id == requestBody.Id);
            if (university == null) throw new MyHttpException(StatusCodes.Status404NotFound, "Event not found");

            university = _mapper.Map(requestBody, university);
            university.UpdatedDate = DateTime.Now;
            _universityRepository.Update(university);
            await _universityRepository.SaveChangesAsync();
            UniversityViewModel universityDetail = _mapper.Map<UniversityViewModel>(university);
            return universityDetail;
        }

        public async Task DeleteUniversityAsync(int id)
        {
            University university = await _universityRepository.GetFirstOrDefaultAsync(alu => alu.Id == id);
            if (university == null) throw new MyHttpException(StatusCodes.Status404NotFound, "Event not found");

            university.Status = (byte?) UniversityEnum.UniversityStatus.Inactive;
            await _universityRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _universityRepository.GetAll().CountAsync();
        }
    }
}