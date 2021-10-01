using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.Business.Services.GroupSrv
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IConfigurationProvider _mapper;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
        }
        public async Task<GroupViewModel> CreateGroup(GroupCreateRequest request, int userId, bool isAdmin)
        {
            var mapper = _mapper.CreateMapper();
            var group = mapper.Map<Group>(request);
            if (isAdmin)
            {
                group.Status = (int)GroupEnum.GroupStatus.Active;
                group.CreatedDate = DateTime.Now;
                group.UpdatedDate = DateTime.Now;
                 _repository.Insert(group);
                await _repository.SaveChangesAsync();
                var groupModel = mapper.Map<GroupViewModel>(group);
                return groupModel;
            }
            else if (group.ParentGroupId != null)
            {
                var parrentGroup = _repository.GetById(group.ParentGroupId);
                if (parrentGroup != null)
                    if (userId == parrentGroup.GroupLeaderId)
                    {
                        group.Status = (int)GroupEnum.GroupStatus.Active;
                        group.CreatedDate = DateTime.Now;
                        group.UpdatedDate = DateTime.Now;
                        _repository.Insert(group);
                        await _repository.SaveChangesAsync();
                        var groupModel = mapper.Map<GroupViewModel>(group);
                        return groupModel;
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

        public List<GroupViewModel> GetGroups(PagingParam<GroupEnum.GroupSortCriteria> paginationModel,
            SearchGroupModel searchGroupModel)
        {
            var queryGroups = _repository.GetAll();
            if (searchGroupModel.GroupName.Length > 0) 
            {
                queryGroups = queryGroups.Where(group => group.GroupName.IndexOf(searchGroupModel.GroupName, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (searchGroupModel.MajorId != null)
                queryGroups = queryGroups.Where(g => g.MajorId == searchGroupModel.MajorId);
            if (searchGroupModel.ParentGroupId != null)
                queryGroups = queryGroups.Where(g => g.ParentGroupId == searchGroupModel.ParentGroupId);
            if (searchGroupModel.UniversityId != null)
                queryGroups = queryGroups.Where(g => g.UniversityId == searchGroupModel.UniversityId);
            queryGroups = queryGroups.Where(group => group.Status == (byte?)searchGroupModel.Status);
            var groupViewModels = queryGroups.ProjectTo<GroupViewModel>(_mapper);
            groupViewModels = groupViewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            return groupViewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();
        }

        public async Task<GroupViewModel> GetGroupById(int id)
        {
            var group = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            var groupModel = _mapper.CreateMapper().Map<GroupViewModel>(group);
            return groupModel;
        }
 
        public async Task<GroupViewModel> UpdateGroup(int id, GroupUpdateRequest request, int userId, bool isAdmin)
        {
            var group = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (group != null)
            {
                if (userId == group.GroupLeaderId || isAdmin)
                {
                    var mapper = _mapper.CreateMapper();
                    group = mapper.Map(request, group);
                    group.UpdatedDate = DateTime.Now;
                    _repository.Update(group);
                    await _repository.SaveChangesAsync();
                    return mapper.Map<GroupViewModel>(group);
                }
            }
            return null;
        }
    }
}
