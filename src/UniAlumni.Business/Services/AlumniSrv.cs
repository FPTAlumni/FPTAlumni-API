using System.Collections.Generic;
using UniAlumni.Business.Services.Interface;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories;

namespace UniAlumni.Business.Services
{
    public class AlumniSrv : IAlumniSvc
    {
        private readonly IAlumniRepository _alumniRepository;

        public AlumniSrv(IAlumniRepository alumniRepository)
        {
            _alumniRepository = alumniRepository;
        }

        public IEnumerable<Alumnus> GetAlumnis()
        {
            return _alumniRepository.GetAll();
        }
    }
}