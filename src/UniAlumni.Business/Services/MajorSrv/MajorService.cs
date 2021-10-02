using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.MajorRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.Business.Services.MajorSrv
{
    public class MajorService : IMajorService
    {
        private readonly IMajorRepository _repository;
        private readonly IConfigurationProvider _mapper;

        public MajorService(IMajorRepository repository, IMapper mapper)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
        }
        public async Task<MajorViewModel> CreateMajor(MajorCreateRequest request)
        {
            var mapper = _mapper.CreateMapper();
            var major = mapper.Map<Major>(request);
            major.Status = (int)MajorEnum.MajorStatus.Active;
            major.CreatedDate = DateTime.Now;
            major.UpdatedDate = DateTime.Now;
            _repository.Insert(major);
            await _repository.SaveChangesAsync();
            var majorModel = mapper.Map<MajorViewModel>(major);
            return majorModel;
        }

        public async Task DeleteMajor(int id)
        {
            var major = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (major != null)
            {
                    major.Status = (int)MajorEnum.MajorStatus.Inactive;
                    major.UpdatedDate = DateTime.Now;
                    _repository.Update(major);
                    await _repository.SaveChangesAsync();
            }
        }

        public async Task<MajorViewModel> GetMajorById(int id)
        {
            var major = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            var majorModel = _mapper.CreateMapper().Map<MajorViewModel>(major);
            return majorModel;
        }

        public List<MajorViewModel> GetMajors(PagingParam<MajorEnum.MajorSortCriteria> paginationModel, SearchMajorModel searchMajorModel)
        {
            var queryMajors = _repository.GetAll();
            if (searchMajorModel.Name.Length > 0)
            {
                queryMajors = queryMajors.Where(m => m.ShortName.IndexOf(searchMajorModel.Name, StringComparison.OrdinalIgnoreCase) >= 0 ||
                m.FullName.IndexOf(searchMajorModel.Name, StringComparison.OrdinalIgnoreCase) >= 0 ||
                m.VietnameseName.IndexOf(searchMajorModel.Name, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (searchMajorModel.UniversityId != null)
                queryMajors = queryMajors.Where(m => m.UniversityId == searchMajorModel.UniversityId);

            queryMajors = queryMajors.Where(m => m.Status == (byte?)searchMajorModel.Status);

            var majorViewModels = queryMajors.ProjectTo<MajorViewModel>(_mapper);
            majorViewModels = majorViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            return majorViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();
        }

        public async Task<MajorViewModel> UpdateMajor(int id, MajorUpdateRequest request)
        {
            var major = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (major != null)
            {
                    var mapper = _mapper.CreateMapper();
                    major = mapper.Map(request, major);
                    major.UpdatedDate = DateTime.Now;
                    _repository.Update(major);
                    await _repository.SaveChangesAsync();
                    return mapper.Map<MajorViewModel>(major);
            }
            return null;
        }
    }
}
