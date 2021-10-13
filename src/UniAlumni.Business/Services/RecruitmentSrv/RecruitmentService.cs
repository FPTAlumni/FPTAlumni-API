﻿using AutoMapper;
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
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
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
        private readonly IAlumniGroupRepository _alumniGroupRepository;

        public RecruitmentService(IRecruitmentRepository repository, IMapper mapper, IGroupRepository groupRepositorty, IAlumniRepository alumniRepository,
            IAlumniGroupRepository alumniGroupRepository)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
            _groupRepositorty = groupRepositorty;
            _alumniRepository = alumniRepository;
            _alumniGroupRepository = alumniGroupRepository;
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

        public async Task<RecruitmentViewModel> GetRecruitmentById(int id, int userId, bool isAdmin)
        {
            //var recruitmentDetail = await _repository.Get(p => p.Id == id)
            //    .ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
            var recruitmentQuery = _repository.Get(r => r.Id == id);
            if (!isAdmin)
            {
                var alumniGroupIds = _alumniGroupRepository.Get(ag => ag.AlumniId == userId).Select(ag => ag.GroupId);
                recruitmentQuery = recruitmentQuery.Where(r => alumniGroupIds.Contains((int)r.GroupId));
                return await recruitmentQuery.ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
            }
            return await recruitmentQuery.ProjectTo<RecruitmentViewModel>(_mapper).FirstOrDefaultAsync();
        }

        public ModelsResponse<RecruitmentViewModel> GetRecruitments(PagingParam<RecruitmentEnum.RecruitmentSortCriteria> paginationModel,
            SearchRecruitmentModel searchRecruitmentModel, int userId, bool isAdmin)
        {
            var queryRecruitments = _repository.GetAll();
            
            if (!isAdmin)
            {
                var alumniGroupIds = _alumniGroupRepository.Get(ag => ag.AlumniId == userId).Select(ag => ag.GroupId);
                queryRecruitments = queryRecruitments.Where(r => alumniGroupIds.Contains((int)r.GroupId) && r.Status == (byte)RecruitmentEnum.RecruitmentStatus.Active);
            }
            else if (searchRecruitmentModel.Status != null)
            {
                queryRecruitments = queryRecruitments.Where(r => r.Status == (byte?)searchRecruitmentModel.Status);
            }
            if (searchRecruitmentModel.MajorId != null)
                queryRecruitments = queryRecruitments.Where(r => r.Group.MajorId == searchRecruitmentModel.MajorId);
            if (searchRecruitmentModel.CompanyId != null)
                queryRecruitments = queryRecruitments.Where(r => r.CompanyId == searchRecruitmentModel.CompanyId);
            if (searchRecruitmentModel.GroupId != null)
                queryRecruitments = queryRecruitments.Where(r => r.GroupId == searchRecruitmentModel.GroupId);
            
            if (searchRecruitmentModel.Type != null)
                queryRecruitments = queryRecruitments.Where(r => r.Type == (byte?)searchRecruitmentModel.Type);
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

        public async Task<RecruitmentViewModel> UpdateRecruitment(RecruitmentUpdateRequest request, int userId, bool isAdmin)
        {
            var recruitment = await _repository.GetFirstOrDefaultAsync(r => r.Id == request.Id);
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
                }
            }
            return null;
        }
    }
}