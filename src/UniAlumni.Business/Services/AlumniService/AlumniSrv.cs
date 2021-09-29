using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.Business.Services.AlumniService
{
    public class AlumniSrv : IAlumniSvc
    {
        private readonly IAlumniRepository _alumniRepository;
        private readonly IMapper _mapper;

        public AlumniSrv(IAlumniRepository alumniRepository, IMapper mapper)
        {
            _alumniRepository = alumniRepository;
            _mapper = mapper;
        }

        public IList<GetAlumniDetail> GetAlumniPage(PagingParam<AlumniEnum.AlumniSortCriteria> paginationModel,
            SearchAlumniModel searchAlumniModel)
        {
            IQueryable<Alumnus> queryAlumni = _alumniRepository.Table;
            if (searchAlumniModel.Email.Length > 0 ||
                searchAlumniModel.Phone.Length > 0 ||
                searchAlumniModel.FullName.Length > 0 ||
                searchAlumniModel.Uid.Length > 0)
            {
                queryAlumni = queryAlumni.Where(alumni => alumni.Email.Contains(searchAlumniModel.Email) &&
                                                          alumni.Phone.Contains(searchAlumniModel.Phone) &&
                                                          alumni.FullName.Contains(searchAlumniModel.FullName) &&
                                                          alumni.Uid.Contains(searchAlumniModel.Uid));
            }

            queryAlumni = queryAlumni.Where(alu => alu.Status == (byte?) searchAlumniModel.Status);

            IQueryable<GetAlumniDetail> queryAlumniDto = _mapper.ProjectTo<GetAlumniDetail>(queryAlumni);
            // Apply Sort
            queryAlumniDto =
                queryAlumniDto.GetWithSorting<GetAlumniDetail>(paginationModel.SortKey.ToString(),
                    paginationModel.SortOrder);
            
            // Apply Paging
            return queryAlumniDto.GetWithPaging<GetAlumniDetail>(paginationModel.Page, paginationModel.PageSize).ToList();
        }

        public async Task<GetAlumniDetail> GetAlumniProfile(int id)
        {
            Alumnus alumnus = await _alumniRepository.GetByIdAsync(id);
            GetAlumniDetail alumniDetail = _mapper.Map<GetAlumniDetail>(alumnus);
            return alumniDetail;
        }

        // public async Task<GetAlumniDetail> CreateAlumni(CreateAlumniRequestBody requestBody)
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}