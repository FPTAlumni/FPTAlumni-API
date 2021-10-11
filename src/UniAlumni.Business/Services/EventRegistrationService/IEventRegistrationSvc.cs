using System.Threading.Tasks;

namespace UniAlumni.Business.Services.EventRegistrationService
{
    public interface IEventRegistrationSvc
    {
        Task JoinEvent(int alumniId, int eventId);
        Task LeaveEvent(int alumniId , int eventId);
    }
}