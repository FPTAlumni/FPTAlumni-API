using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.EventRepo
{
    public class EventRepository : BaseRepository<Event> , IEventRepository
    {
        public EventRepository(DbContext context) : base(context)
        {
        }

        public EventRepository(DbContext context, DbSet<Event> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}