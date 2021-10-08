using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.Business.Services.GroupSrv
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IConfigurationProvider _mapper;
        private readonly IAlumniGroupRepository _alumniGroupRepository;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
        }
        public async Task<GroupViewModel> CreateGroup(GroupCreateRequest request, int userId, bool isAdmin)
        {
            var mapper = _mapper.CreateMapper();
            var group = mapper.Map<Group>(request);
            if (group.ParentGroupId == null)
            {
                if (isAdmin)
                {
                    group.Status = (int)GroupEnum.GroupStatus.Active;
                    _repository.Insert(group);
                    await _repository.SaveChangesAsync();
                    return await _repository.Get(g => g.Id == group.Id).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
                }
            }
            else 
            {
                var parrentGroup = _repository.GetById(group.ParentGroupId);
                if (parrentGroup != null && parrentGroup.ParentGroupId == null)
                    if (userId == parrentGroup.GroupLeaderId || isAdmin)
                    {
                        group.Status = (int)GroupEnum.GroupStatus.Active;
                        group.UniversityMajorId = parrentGroup.UniversityMajorId;
                        _repository.Insert(group);
                        await _repository.SaveChangesAsync();
                        return await _repository.Get(g => g.Id == group.Id).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
                    }
            }
            return null;
        }

        public async Task DeleteGroup(int id, int userId, bool isAdmin)
        {
            var group = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (group != null)
            {
                if (userId == group.GroupLeaderId || isAdmin)
                {
                    group.Status = (int)GroupEnum.GroupStatus.Inactive;
                    group.UpdatedDate = DateTime.Now;
                    _repository.Update(group);
                    await _repository.SaveChangesAsync();
                }
            }
        }

        public ModelsResponse<GroupViewModel> GetGroups(PagingParam<GroupEnum.GroupSortCriteria> paginationModel,
            SearchGroupModel searchGroupModel, int universityId)
        {
            var queryGroups = _repository.Get(g => g.UniversityMajor.UniversityId == universityId &&
                                    g.Status == (byte?)searchGroupModel.Status);
            if (searchGroupModel.AlumniId != null)
                queryGroups = queryGroups.Where(g => g.AlumniGroups.Any(ag => ag.AlumniId == searchGroupModel.AlumniId));
            if (searchGroupModel.GroupName.Length > 0)             
                queryGroups = queryGroups.Where(group => group.GroupName.IndexOf(searchGroupModel.GroupName, StringComparison.OrdinalIgnoreCase) >= 0);            
            if (searchGroupModel.MajorId != null)
                queryGroups = queryGroups.Where(g => g.UniversityMajor.MajorId == searchGroupModel.MajorId);
            if (searchGroupModel.ParentGroupId != null)
                queryGroups = queryGroups.Where(g => g.ParentGroupId == searchGroupModel.ParentGroupId);

            var groupViewModels = queryGroups.ProjectTo<GroupViewModel>(_mapper);
            groupViewModels = groupViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            var data = groupViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<GroupViewModel>()
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

        public async Task<GroupViewModel> GetGroupById(int id, int universityId)
        {
            var groupModel = await _repository.Get(g => g.Id == id && g.UniversityMajor.UniversityId == universityId).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
            return groupModel;
        }
 
        public async Task<GroupViewModel> UpdateGroup(GroupUpdateRequest request, int userId, bool isAdmin)
        {
            var group = await _repository.GetFirstOrDefaultAsync(p => p.Id == request.Id);
            if (group != null)
            {
                if (userId == group.GroupLeaderId || isAdmin)
                {
                    var mapper = _mapper.CreateMapper();
                    group = mapper.Map(request, group);
                    group.UpdatedDate = DateTime.Now;
                    _repository.Update(group);
                    await _repository.SaveChangesAsync();
                    return await _repository.Get(g => g.Id == request.Id).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
                }
            }
            return null;
        }
    }
}
