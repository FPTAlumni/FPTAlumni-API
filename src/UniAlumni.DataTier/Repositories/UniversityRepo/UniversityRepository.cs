using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.UniversityRepo
{
    public class UniversityRepository : BaseRepository<University>, IUniversityRepository
    {
        public UniversityRepository(DbContext context) : base(context)
        {
        }

        public UniversityRepository(DbContext context, DbSet<University> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}