using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.EventRepo;

namespace UniAlumni.DataTier.Repositories.EventRegistrationRepo
{
    public class EventRegistrationRepository : BaseRepository<EventRegistration> , IEventRegistrationRepository
    {
        public EventRegistrationRepository(DbContext context) : base(context)
        {
        }

        public EventRegistrationRepository(DbContext context, DbSet<EventRegistration> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}