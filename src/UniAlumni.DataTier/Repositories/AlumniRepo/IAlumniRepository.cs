using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.Interface;
using UniAlumni.DataTier.ViewModels.Alumni;


namespace UniAlumni.DataTier.Repositories.AlumniRepo
{
    public interface IAlumniRepository : IBaseRepository<Alumnus>
    {
        public Alumnus GetByEmail(string email);
        public Task<Alumnus> GetByEmailAsync(string email);
        public Task<Alumnus> GetByUidAsync(string uid);
        public Task<Alumnus> CreateAlumniAsync(Alumnus newAlumnus);
        public Task<Alumnus> UpdateAlumniAsync(Alumnus updateAlumni);
        public Task DeleteAlumniAsync(int id);
        public Task ActivateAlumniAsync(ActivateAlumniRequestBody requestBody);

    }
}