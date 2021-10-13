﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.ClassMajorRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.Business.Services.AlumniService
{
    public class AlumniSrv : IAlumniSvc
    {
        private readonly IAlumniRepository _alumniRepository;
        private readonly IAlumniGroupRepository _alumniGroupRepository;
        private readonly IClassMajorRepository _classMajorRepository;
        private readonly IMapper _mapper;

        public AlumniSrv(IAlumniRepository alumniRepository, IMapper mapper,
            IAlumniGroupRepository alumniGroupRepository,
            IClassMajorRepository classMajorRepository)
        {
            _classMajorRepository = classMajorRepository;
            _alumniGroupRepository = alumniGroupRepository;
            _alumniRepository = alumniRepository;
            _mapper = mapper;
        }

        public IList<GetAlumniDetail> GetAlumniPage(PagingParam<AlumniEnum.AlumniSortCriteria> paginationModel,
            SearchAlumniModel searchAlumniModel)
        {
            IQueryable<Alumnus> queryAlumni = _alumniRepository.Table
                .Include(a => a.ClassMajor)
                    .ThenInclude(cm => cm.Class)
                        .ThenInclude(c=>c.University)
                .Include(a => a.Company)
                .Include(a => a.ClassMajor)
                    .ThenInclude(cm=> cm.Major);
            if (searchAlumniModel.Email is {Length: > 0})
                queryAlumni = queryAlumni.Where(alu => alu.Email.Contains(searchAlumniModel.Email));

            if (searchAlumniModel.Phone is {Length: > 0})
                queryAlumni = queryAlumni.Where(alu => alu.Phone.Contains(searchAlumniModel.Phone));

            if (searchAlumniModel.FullName is {Length: > 0})
                queryAlumni = queryAlumni.Where(alu => alu.FullName.Contains(searchAlumniModel.FullName));

            if (searchAlumniModel.Uid is {Length: > 0})
                queryAlumni = queryAlumni.Where(alu => alu.Uid.Contains(searchAlumniModel.Uid));

            if (searchAlumniModel.Status != null)
                queryAlumni = queryAlumni.Where(alu => alu.Status == (byte?) searchAlumniModel.Status);

            // Apply GroupId
            if (searchAlumniModel.GroupId != null)
            {
                IQueryable<AlumniGroup> queryAlumniGroup =
                    _alumniGroupRepository.Table.Where(ag => ag.GroupId == searchAlumniModel.GroupId &&
                                                             ag.Status == (byte?) AlumniGroupEnum.AlumniGroupStatus
                                                                 .Active);
                List<int> listAlumniIdInGroup = queryAlumniGroup.Select(ag => ag.AlumniId).ToList();
                if (listAlumniIdInGroup.Count != 0)
                {
                    queryAlumni = queryAlumni.Where(alu => listAlumniIdInGroup.Contains(alu.Id));
                }
            }


            // Apply Sort
            if (paginationModel.SortKey.ToString().Trim().Length > 0)
                queryAlumni = queryAlumni.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);
            // Apply Paging
            queryAlumni = queryAlumni.GetWithPaging(paginationModel.Page, paginationModel.PageSize).AsQueryable();


            IQueryable<GetAlumniDetail> queryAlumniDto = _mapper.ProjectTo<GetAlumniDetail>(queryAlumni);
            // Apply Paging
            return queryAlumniDto.ToList();
        }

        public async Task<GetAlumniDetail> GetAlumniProfile(int id)
        {
            Alumnus alumnus = await _alumniRepository.Get(a => a.Id == id)
                .Include(a => a.ClassMajor)   
                    .ThenInclude(cm => cm.Class)
                        .ThenInclude(c=>c.University)
                    .Include(a => a.Company)
                    .Include(a => a.ClassMajor)
                        .ThenInclude(cm=> cm.Major)
               .FirstOrDefaultAsync();
            GetAlumniDetail alumniDetail = _mapper.Map<GetAlumniDetail>(alumnus);
            return alumniDetail;
        }

        public async Task<GetAlumniDetail> CreateAlumniAsync(CreateAlumniRequestBody requestBody)
        {
            Alumnus alumnus = _mapper.Map<Alumnus>(requestBody);
            ClassMajor classMajor = await _classMajorRepository.Get(cm => cm.ClassId == requestBody.ClassId &&
                                                                    cm.MajorId == requestBody.MajorId)
                .FirstOrDefaultAsync();
            if (classMajor != null)
            {
                alumnus.ClassMajorId = classMajor.Id;
            }
            else
            {
                throw new Exception("No ClassMajor Exist");
               
            }
            UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(alumnus.Uid);
            if (user != null)
            {
                alumnus.Email = user.Email;
            }

            alumnus.Status = (byte?) AlumniEnum.AlumniStatus.Pending;
            alumnus = await _alumniRepository.CreateAlumniAsync(alumnus);
            alumnus = await _alumniRepository.Get(a => a.Id == alumnus.Id)
                .Include(a => a.ClassMajor)   
                .ThenInclude(cm => cm.Class)
                .ThenInclude(c=>c.University)
                .Include(a => a.Company)
                .Include(a => a.ClassMajor)
                .ThenInclude(cm=> cm.Major)
                .FirstOrDefaultAsync();
            GetAlumniDetail alumniDetail = _mapper.Map<GetAlumniDetail>(alumnus);
            return alumniDetail;
        }

        public async Task<GetAlumniDetail> UpdateAlumniAsync(UpdateAlumniRequestBody requestBody)
        {
            Alumnus alumnus = await _alumniRepository.GetFirstOrDefaultAsync(alu => alu.Id == requestBody.Id);
            alumnus = _mapper.Map(requestBody, alumnus);
            alumnus.UpdatedDate = DateTime.Now;
            Alumnus updateAlumni = await _alumniRepository.UpdateAlumniAsync(alumnus);
            GetAlumniDetail alumniDetail = _mapper.Map<GetAlumniDetail>(updateAlumni);
            return alumniDetail;
        }

        public async Task ActivateAlumniAsync(ActivateAlumniRequestBody requestBody)
        {
            await _alumniRepository.ActivateAlumniAsync(requestBody);
        }

        public async Task DeleteAlumniAsync(int id)
        {
            await _alumniRepository.DeleteAlumniAsync(id);
        }

        public async Task<int> GetTotal()
        {
            return await _alumniRepository.GetAll().CountAsync();
        }
    }
}