using System.Threading.Tasks;
using UniAlumni.DataTier.Common;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.AlumniGroup;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.Business.Services.GroupSrv
{
    public interface IGroupService
    {
        ModelsResponse<T> GetGroups<T>(PagingParam<GroupEnum.GroupSortCriteria> paginationModel,
            SearchGroupModel searchGroupModel, int userId, bool isAdmin) where T:class;
        ModelsResponse<AlumniGroupGroupDetailModel<T>> GetGroupsByAlumniId<T>(PagingParam<GroupEnum.GroupSortCriteria> paginationModel,
            SearchGroupModel searchGroupModel, int userId, bool isAdmin) where T : class;
        Task<GroupViewModel> GetGroupById(int id, int userId, bool isAdmin);
        ModelsResponse<AlumniGroupViewModel> GetGroupMember(PagingParam<AlumniGroupEnum.AlumniGroupSortCriteria> paginationModel,
            SearchAlumniGroupModel searchAlumniGroupModel, int groupId, int userId, bool isAdmin);
        Task<AlumniGroupViewModel> UpdateGroupMember(AlumniGroupUpdateRequest request, int groupId, int userId, bool isAdmin);
        Task<GroupViewModel> CreateGroup(GroupCreateRequest request, int userId, bool isAdmin);
        Task<GroupViewModel> UpdateGroup(GroupUpdateRequest request, int userId, bool isAdmin);
        Task DeleteGroup(int id, int userId, bool isAdmin);
    }
}
