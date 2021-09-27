using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Request.Group;
using UniAlumni.DataTier.ViewModels;

namespace UniAlumni.Business.Services.GroupService
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
                group.Status = (int)GroupStatus.Active;
                await _repository.InsertAsync(group);
                var groupModel = mapper.Map<GroupViewModel>(group);
                return groupModel;
            }
            else if (group.ParentGroupId != null)
            {
                var parrentGroup = _repository.GetById(group.ParentGroupId);
                if (parrentGroup != null)
                    if (userId == parrentGroup.GroupLeaderId)
                    {
                        group.Status = (int)GroupStatus.Active;
                        await _repository.InsertAsync(group);
                        var groupModel = mapper.Map<GroupViewModel>(group);
                        return groupModel;
                    }
            }
            return null;
        }

        public async Task DeleteGroup(int id)
        {
            var group = _repository.Get(p => p.Id == id).FirstOrDefault();
            if (group != null)
            {
                group.Status = (int)GroupStatus.Inactive;
                group.UpdatedDate = DateTime.Now;
                _repository.Update(group);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<List<GroupViewModel>> GetAllGroups(PaginationModel paginationModel)
        {
            var result = _repository.Get(p => p.Status == (int)GroupStatus.Active)
                .ProjectTo<GroupViewModel>(_mapper)
                .PagingIQueryable<GroupViewModel>(paginationModel);
            return await result.ToListAsync();
        }

        public async Task<GroupViewModel> GetGroupById(int id)
        {
            var groupModel = await _repository.Get(p => p.Id == id && p.Status == (int)GroupStatus.Active).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
            return groupModel;
        }
        public async Task<List<GroupViewModel>> GetGroups(PaginationModel paginationModel, string groupName)
        {
            var result = _repository.Get(p => p.GroupName.Contains(groupName) && p.Status == (int)GroupStatus.Active)
                .ProjectTo<GroupViewModel>(_mapper)
                .PagingIQueryable<GroupViewModel>(paginationModel);
            return await result.ToListAsync();
        }

        public async Task<GroupViewModel> UpdateGroup(int id, GroupUpdateRequest request, int userId, bool isAdmin)
        {
            var group = await _repository.Get(p => p.Id == id).FirstOrDefaultAsync();
            if (group != null)
            {
                if (userId == group.GroupLeaderId || isAdmin)
                {
                    var mapper = _mapper.CreateMapper();
                    var requestGroup = mapper.Map<Group>(request);
                    group.GroupName = requestGroup.GroupName;
                    group.GroupLeaderId = requestGroup.GroupLeaderId;
                    group.ParentGroupId = requestGroup.ParentGroupId;
                    group.MajorId = requestGroup.MajorId;
                    group.Banner = requestGroup.Banner;
                    group.Status = requestGroup.Status;
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
