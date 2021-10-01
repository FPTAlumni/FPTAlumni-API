using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.CategoryRepo
{
    public class CategoryRepository : BaseRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public CategoryRepository(DbContext context, DbSet<Category> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}