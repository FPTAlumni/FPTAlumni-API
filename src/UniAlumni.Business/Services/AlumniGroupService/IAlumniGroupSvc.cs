using System.Threading.Tasks;

namespace UniAlumni.Business.Services.AlumniGroupService
{
    public interface IAlumniGroupSvc
    {
        // Request to Join Group
        Task JoinGroup(int alumniId, int groupId);
        
        // Cancel Request Join Group
        Task CancelRequestJoinGroup(int alumniId, int groupId);
        Task LeaveGroup(int alumniId , int groupId);
        // Task UpdateStatus(int id, int alumnId,int groupId, int status);
    }
}