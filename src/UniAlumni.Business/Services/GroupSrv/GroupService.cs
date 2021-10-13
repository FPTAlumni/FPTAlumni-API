using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.AlumniGroup;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.Business.Services.GroupSrv
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IConfigurationProvider _mapper;
        private readonly IAlumniGroupRepository _alumniGroupRepository;
        private readonly IAlumniRepository _alumniRepository;

        public GroupService(IGroupRepository repository, IMapper mapper, IAlumniGroupRepository alumniGroupRepository, IAlumniRepository alumniRepository)
        {
            _mapper = mapper.ConfigurationProvider;
            _repository = repository;
            _alumniGroupRepository = alumniGroupRepository;
            _alumniRepository = alumniRepository;
        }
        public async Task<GroupViewModel> CreateGroup(GroupCreateRequest request, int userId, bool isAdmin)
        {
            var mapper = _mapper.CreateMapper();
            var group = mapper.Map<Group>(request);
            if (group.ParentGroupId == null)
            {
                if (isAdmin)
                {
                    group.Status = (byte)GroupEnum.GroupStatus.Active;
                    _repository.Insert(group);
                    await _repository.SaveChangesAsync();
                    return await _repository.Get(g => g.Id == group.Id).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
                }
                else
                    throw new MyHttpException(StatusCodes.Status403Forbidden, "Does not have access rights to the content");
            }
            else
            {
                var parrentGroup = _repository.GetById(group.ParentGroupId);
                if (parrentGroup == null)
                    throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching parent group");
                if (parrentGroup.ParentGroupId != null)
                    throw new MyHttpException(StatusCodes.Status400BadRequest, "A sub-group cannot have its sub-group");

                if (userId == parrentGroup.GroupLeaderId || isAdmin)
                {
                    group.Status = (byte)GroupEnum.GroupStatus.Active;
                    group.UniversityId = parrentGroup.UniversityId;
                    group.MajorId = parrentGroup.MajorId;
                    _repository.Insert(group);
                    await _repository.SaveChangesAsync();
                    return await _repository.Get(g => g.Id == group.Id).ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
                }
                else
                    throw new MyHttpException(StatusCodes.Status403Forbidden, "Does not have access rights to the content");

            }
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
                else
                    throw new MyHttpException(StatusCodes.Status403Forbidden, "Does not have access rights to the content");
            }
            else
            {
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching group member");
            }
        }

        public ModelsResponse<AlumniGroupViewModel> GetGroupMember(PagingParam<AlumniGroupEnum.AlumniGroupSortCriteria> paginationModel,
            SearchAlumniGroupModel searchAlumniGroupModel, int groupId, int userId, bool isAdmin)
        {
            var userGroupIds = _alumniGroupRepository.Get(ag => ag.AlumniId == userId).Select(ag => ag.GroupId);
            var groupLeaderId = _repository.Get(g => g.Id == groupId).Select(g => g.GroupLeaderId).FirstOrDefault();

            IQueryable<AlumniGroup> alumniGroupQuery = null;
            if (!isAdmin && groupLeaderId != userId && userGroupIds.Contains(groupId))
            {
                alumniGroupQuery = _alumniGroupRepository.Get(ag => ag.GroupId == groupId && ag.Status == (byte)AlumniGroupEnum.AlumniGroupStatus.Active);
            }
            else if (isAdmin || groupLeaderId == userId)
            {
                alumniGroupQuery = _alumniGroupRepository.Get(ag => ag.GroupId == groupId);
                if (searchAlumniGroupModel.Status != null)
                    alumniGroupQuery = alumniGroupQuery.Where(ag => ag.Status == (byte?)searchAlumniGroupModel.Status);
            }
            if (searchAlumniGroupModel.FullName.Length > 0)
                alumniGroupQuery = alumniGroupQuery.Where(ag => ag.Alumni.FullName.IndexOf(searchAlumniGroupModel.FullName, StringComparison.OrdinalIgnoreCase) >= 0);
            if (searchAlumniGroupModel.Email.Length > 0)
                alumniGroupQuery = alumniGroupQuery.Where(ag => ag.Alumni.Email.Contains(searchAlumniGroupModel.Email));
            if (searchAlumniGroupModel.RegisteredFromDate != null)
                alumniGroupQuery = alumniGroupQuery.Where(ag => ag.RegisteredDate >= searchAlumniGroupModel.RegisteredFromDate);
            if (searchAlumniGroupModel.RegisteredToDate != null)
                alumniGroupQuery = alumniGroupQuery.Where(ag => ag.RegisteredDate <= searchAlumniGroupModel.RegisteredToDate);


            var viewModels = alumniGroupQuery.ProjectTo<AlumniGroupViewModel>(_mapper);

            viewModels = viewModels.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            var data = viewModels.GetWithPaging(paginationModel.Page, paginationModel.PageSize).ToList();

            return new ModelsResponse<AlumniGroupViewModel>()
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
        public async Task<AlumniGroupViewModel> UpdateGroupMember(AlumniGroupUpdateRequest request, int groupId, int userId, bool isAdmin)
        {
            var group = _repository.GetFirstOrDefault(g => g.Id == groupId);
            if (group == null)
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching group");
            if (isAdmin || group.GroupLeaderId == userId)
            {
                var alumniGroup = _alumniGroupRepository.Get(ag => ag.AlumniId == request.AlumniId && ag.GroupId == groupId).FirstOrDefault();
                if (alumniGroup != null)
                {
                    alumniGroup.Status = request.Status;
                    _alumniGroupRepository.Update(alumniGroup);
                    await _alumniGroupRepository.SaveChangesAsync();
                    return await _alumniGroupRepository.Get(ag => ag.AlumniId == request.AlumniId && ag.GroupId == groupId)
                        .ProjectTo<AlumniGroupViewModel>(_mapper).FirstOrDefaultAsync();
                }
                else
                {
                    throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching group member");
                }
            }
            else
                throw new MyHttpException(StatusCodes.Status403Forbidden, "Does not have access rights to the content");
        }

        public ModelsResponse<GroupViewModel> GetGroups(PagingParam<GroupEnum.GroupSortCriteria> paginationModel,
            SearchGroupModel searchGroupModel, int userId, bool isAdmin)
        {
            var queryGroups = _repository.GetAll();
            if (!isAdmin)
            {
                var alumniUniversityId = _alumniRepository.Get(a => a.Id == userId).Select(a => a.ClassMajor.Class.UniversityId).FirstOrDefault();
                queryGroups = queryGroups.Where(g => g.UniversityId == alumniUniversityId && g.Status == (byte)GroupEnum.GroupStatus.Active);
            }
            else if (searchGroupModel.Status != null)
            {
                queryGroups = queryGroups.Where(g => g.Status == (byte)searchGroupModel.Status);
            }
            if (searchGroupModel.AlumniId != null)
                queryGroups = queryGroups.Where(g => g.AlumniGroups.Any(ag => ag.AlumniId == searchGroupModel.AlumniId));
            if (searchGroupModel.GroupLeaderId != null)
                queryGroups = queryGroups.Where(g => g.GroupLeaderId == searchGroupModel.GroupLeaderId);
            if (searchGroupModel.GroupName.Length > 0)
                queryGroups = queryGroups.Where(group => group.GroupName.IndexOf(searchGroupModel.GroupName, StringComparison.OrdinalIgnoreCase) >= 0);
            if (searchGroupModel.MajorId != null)
                queryGroups = queryGroups.Where(g => g.MajorId == searchGroupModel.MajorId);
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

        public async Task<GroupViewModel> GetGroupById(int id, int universityId, bool isAdmin)
        {
            var groups = _repository.Get(g => g.Id == id && g.UniversityId == universityId);
            if (!isAdmin)
            {
                groups = groups.Where(g => g.Status == (byte)GroupEnum.GroupStatus.Active);
            }
            return await groups.ProjectTo<GroupViewModel>(_mapper).FirstOrDefaultAsync();
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
                else
                    throw new MyHttpException(StatusCodes.Status403Forbidden, "Does not have access rights to the content");
            }
            else
            {
                throw new MyHttpException(StatusCodes.Status400BadRequest, "Cannot find matching group member");
            }
        }
    }
}
