using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories
{
    public class AlumniRepository : BaseRepository<Alumnus> , IAlumniRepository
    {
        public AlumniRepository(DbContext context) : base(context)
        {
        }

        public AlumniRepository(DbContext context, DbSet<Alumnus> dbsetExist) : base(context, dbsetExist)
        {
        }

        public Alumnus GetByEmail(string email)
        {
            IQueryable<Alumnus> query = Table;
            Alumnus alumnus = query.FirstOrDefault(x => x.Email == email);
            // Alumnus alumnus = query.Where()
            return alumnus;
        }

        public async Task<Alumnus> GetByEmailAsync(string email)
        {
            IQueryable<Alumnus> query = Table;
            return await query.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}