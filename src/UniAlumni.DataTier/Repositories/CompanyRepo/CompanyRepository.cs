using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.CompanyRepo
{
    public class CompanyRepository : BaseRepository<Company> , ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context)
        {
        }

        public CompanyRepository(DbContext context, DbSet<Company> dbsetExist) : base(context, dbsetExist)
        {
        }
    }
}