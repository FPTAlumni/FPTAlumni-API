using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories
{
    public interface IAlumniRepository : IBaseRepository<Alumnus>
    {
        public Alumnus GetByEmail(string email);
        public Task<Alumnus> GetByEmailAsync(string email);
    }
}