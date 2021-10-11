using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.AlumniGroupRepo
{
    public class AlumniGroupRepository : BaseRepository<AlumniGroup> , IAlumniGroupRepository
    {
        public AlumniGroupRepository(DbContext context) : base(context)
        {
        }

        public AlumniGroupRepository(DbContext context, DbSet<AlumniGroup> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}