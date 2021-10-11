using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.ClassRepo
{
    public class ClassRepository : BaseRepository<Class>  , IClassRepository
    {
        public ClassRepository(DbContext context) : base(context)
        {
        }

        public ClassRepository(DbContext context, DbSet<Class> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}