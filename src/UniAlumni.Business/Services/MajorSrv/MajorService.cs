using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
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
            var majorModel = await _repository.Get(m => m.Id == id).ProjectTo<MajorViewModel>(_mapper).FirstOrDefaultAsync();
            return majorModel;
        }

        public ModelsResponse<MajorViewModel> GetMajors(PagingParam<MajorEnum.MajorSortCriteria> paginationModel, SearchMajorModel searchMajorModel)
        {
            var queryMajors = _repository.GetAll();

            if (searchMajorModel.Name.Length > 0)
            {
                queryMajors = queryMajors.Where(m => m.ShortName.IndexOf(searchMajorModel.Name, StringComparison.OrdinalIgnoreCase) >= 0 ||
                m.FullName.IndexOf(searchMajorModel.Name, StringComparison.OrdinalIgnoreCase) >= 0 ||
                m.VietnameseName.IndexOf(searchMajorModel.Name, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (searchMajorModel.ClassId != null)
            {
                queryMajors = queryMajors.Where(m => m.ClassMajors.Any(cm => cm.ClassId == searchMajorModel.ClassId));
            }
            if (searchMajorModel.Status != null)
            {
                queryMajors = queryMajors.Where(m => m.Status == (byte?)searchMajorModel.Status);
            }

            var majorViewModels = queryMajors.ProjectTo<MajorViewModel>(_mapper);
            majorViewModels = majorViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            var data = majorViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();
            return new ModelsResponse<MajorViewModel>()
            {
                Code = StatusCodes.Status200OK,
                Msg = "",
                Data = data,
                Metadata = new PagingMetadata()
                {
                    Page = paginationModel.Page,
                    Size = paginationModel.PageSize,
                    Total = data.Count
                }
            };
        }

        public async Task<MajorViewModel> UpdateMajor(MajorUpdateRequest request)
        {
            var major = await _repository.GetFirstOrDefaultAsync(p => p.Id == request.Id);
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
