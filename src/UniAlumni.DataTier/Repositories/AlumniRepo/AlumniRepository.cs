using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.DataTier.Repositories.AlumniRepo
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

        public async Task<Alumnus> GetByUidAsync(string uid)
        {
            Alumnus alumnusDb = await GetFirstOrDefaultAsync(alu=> alu.Uid == uid);
            
            return alumnusDb;
        }
        
        public async Task<Alumnus> CreateAlumniAsync(Alumnus newAlumnus)
        {
            await InsertAsync(newAlumnus);

            await SaveChangesAsync();
            
            return newAlumnus;
        }

        public async Task<Alumnus> UpdateAlumniAsync(Alumnus updateAlumni)
        {
            
            
            Update(updateAlumni);

            await SaveChangesAsync();

            return updateAlumni;
        }

        public async Task DeleteAlumniAsync(int id)
        {
            Alumnus alumnusDb = await GetFirstOrDefaultAsync(alu => alu.Id == id);
            alumnusDb.Status = (byte?) AlumniEnum.AlumniStatus.Deactive;

            await SaveChangesAsync();
        }
        
        public async Task ActivateAlumniAsync(ActivateAlumniRequestBody requestBody){
            Alumnus alumnus = await GetFirstOrDefaultAsync(alu => alu.Id.Equals(requestBody.Id));
            if (alumnus != null)
            {
                alumnus.Status = (byte?) requestBody.Status;
            }

            await SaveChangesAsync();
        }
    }
}