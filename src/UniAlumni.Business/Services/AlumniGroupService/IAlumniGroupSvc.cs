using System.Threading.Tasks;

namespace UniAlumni.Business.Services.AlumniGroupService
{
    public interface IAlumniGroupSvc
    {
        Task JoinGroup(int alumniId, int groupId);
        Task LeaveGroup(int alumniId , int groupId);
        // Task UpdateStatus(int id, int alumnId,int groupId, int status);
    }
}