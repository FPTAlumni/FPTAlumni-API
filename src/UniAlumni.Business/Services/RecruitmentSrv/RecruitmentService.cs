using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Repositories.RecruitmentRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Recruitment;

namespace UniAlumni.Business.Services.RecruitmentSrv
{
    public class RecruitmentService : IRecruitmentService
    {
        private readonly IRecruitmentRepository _repository;
        private readonly IConfigurationProvider _mapper;
        private readonly IGroupRepository _groupRepositorty;
        private readonly IAlumniRepository _alumniRepository;

        public RecruitmentService(IRecruitmentRepository repository, IMapper mapper, IGroupRepository groupRepositorty, IAlumniRepository alumniRepository)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
            _groupRepositorty = groupRepositorty;
            _alumniRepository = alumniRepository;
        }
        public async Task<RecruitmentViewModel> CreateRecruitment(RecruitmentCreateRequest request, int userId, bool isAdmin)
        {
            Group group = _groupRepositorty.GetById(request.GroupOriginId);
            if (group != null)
            {
                var mapper = _mapper.CreateMapper();
                var recruitment = mapper.Map<Recruitment>(request);
                if (isAdmin || userId == group.GroupLeaderId)
                {
                    recruitment.Status = (byte?)RecruitmentEnum.RecruitmentStatus.Active;                   
                }
                else
                {
                    recruitment.AlumniId = userId;
                    recruitment.CompanyId = _alumniRepository.GetById(userId).CompanyId;
                    recruitment.Status = (byte?)RecruitmentEnum.RecruitmentStatus.Pending;
                }
                _repository.Insert(recruitment);
                await _repository.SaveChangesAsync();
                var viewModel = await _repository.Get(r => r.Id == recruitment.Id).ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
                return viewModel;
            }
            return null;
        }

        public async Task DeleteRecruitment(int id, int userId, bool isAdmin)
        {
            var recruitment = await _repository.GetFirstOrDefaultAsync(r => r.Id == id);
            if (recruitment != null)
            {
                Group group = _groupRepositorty.GetById(recruitment.GroupOriginId);
                if (group != null)
                {
                    if (isAdmin || userId == group.GroupLeaderId || userId == recruitment.AlumniId)
                    {
                        recruitment.Status = (byte?)RecruitmentEnum.RecruitmentStatus.Inactive;
                        recruitment.UpdatedDate = DateTime.Now;
                        _repository.Update(recruitment);
                        await _repository.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task<RecruitmentViewModel> GetRecruitmentById(int id, int universityId)
        {
            var recruitmentDetail = await _repository.Get(p => p.Id == id && p.Group.UniversityMajor.UniversityId == universityId)
                .ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
            return recruitmentDetail;
        }

        public ModelsResponse<RecruitmentViewModel> GetRecruitments(PagingParam<RecruitmentEnum.RecruitmentSortCriteria> paginationModel, SearchRecruitmentModel searchRecruitmentModel, int universityId)
        {
            var queryRecruitments = _repository.Get(r => r.Group.UniversityMajor.UniversityId == universityId &&
                                    r.Status == (byte?)searchRecruitmentModel.Status &&
                                    r.Type == (byte?)searchRecruitmentModel.Type);
            if (searchRecruitmentModel.MajorId != null)
                queryRecruitments = queryRecruitments.Where(r => r.MajorId == searchRecruitmentModel.MajorId);
            if (searchRecruitmentModel.CompanyId != null)
                queryRecruitments = queryRecruitments.Where(r => r.CompanyId == searchRecruitmentModel.CompanyId);
            if (searchRecruitmentModel.GroupId != null)
                queryRecruitments = queryRecruitments.Where(r => r.GroupId == searchRecruitmentModel.GroupId);
            var recruitmentViewModels = queryRecruitments.ProjectTo<RecruitmentViewModel>(_mapper);
            recruitmentViewModels = recruitmentViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            var data = recruitmentViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<RecruitmentViewModel>()
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

        public async Task<RecruitmentViewModel> UpdateRecruitment(int id, RecruitmentUpdateRequest request, int userId, bool isAdmin)
        {
            var recruitment = await _repository.GetFirstOrDefaultAsync(r => r.Id == id);
            var mapper = _mapper.CreateMapper();
            if (recruitment != null)
            {
                Group group = _groupRepositorty.GetById(recruitment.GroupOriginId);
                if (group != null)
                {
                    
                    if (isAdmin || userId == group.GroupLeaderId)
                    {
                        recruitment = mapper.Map(request, recruitment);
                        recruitment.UpdatedDate = DateTime.Now;
                        _repository.Update(recruitment);
                        await _repository.SaveChangesAsync();
                        var viewModel = await _repository.Get(r => r.Id == recruitment.Id).ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
                        return viewModel;
                    }
                    else if (userId == recruitment.AlumniId)
                    {
                        byte? tmp = recruitment.Status;
                        recruitment = mapper.Map(request, recruitment);
                        recruitment.AlumniId = userId;
                        recruitment.CompanyId = _alumniRepository.GetById(userId).CompanyId;
                        recruitment.Status = tmp;
                        recruitment.UpdatedDate = DateTime.Now;
                        _repository.Update(recruitment);
                        await _repository.SaveChangesAsync();
                        var viewModel = await _repository.Get(r => r.Id == recruitment.Id).ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
                        return viewModel;
                    }
                }
            }
            return null;
        }
    }
}
