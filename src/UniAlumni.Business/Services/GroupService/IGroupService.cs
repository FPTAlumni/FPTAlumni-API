using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Request.Group;
using UniAlumni.DataTier.ViewModels;

namespace UniAlumni.Business.Services.GroupService
{
    public interface IGroupService
    {
        Task<List<GroupViewModel>> GetAllGroups(PaginationModel paginationModel);
        Task<GroupViewModel> GetGroupById(int id);
        Task<GroupViewModel> CreateGroup(GroupCreateRequest request, int userId, bool isAdmin);
        Task<GroupViewModel> UpdateGroup(int id, GroupUpdateRequest request, int userId, bool isAdmin);
        Task DeleteGroup(int id);
    }
}
