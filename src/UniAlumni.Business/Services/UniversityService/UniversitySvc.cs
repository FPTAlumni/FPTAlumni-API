using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UniAlumni.DataTier.Common.Enum;
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

        public IList<UniversityViewModel> GetAllUniversity(PagingParam<UniversityEnum.UniversitySortCriteria> paginationModel, SearchUniversityModel searchUniversityModel)
        {
            IQueryable<University> queryUniversity = _universityRepository.Table;
            if (searchUniversityModel.Name.Length > 0)
            {
                queryUniversity = queryUniversity.Where(university => university.Name.Contains(searchUniversityModel.Name));
            }

            IQueryable<UniversityViewModel> queryUniversityDto = _mapper.ProjectTo<UniversityViewModel>(queryUniversity);
            // Apply Sort
            queryUniversityDto =
                queryUniversityDto.GetWithSorting(paginationModel.SortKey.ToString(),
                    paginationModel.SortOrder);

            // Apply Paging
            return queryUniversityDto.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();
        }

        public async Task<UniversityViewModel> GetUniversityById(int id)
        {
            University university = await _universityRepository.GetByIdAsync(id);
            UniversityViewModel universityDetail = _mapper.Map<UniversityViewModel>(university);
            return universityDetail;
        }
    }
}