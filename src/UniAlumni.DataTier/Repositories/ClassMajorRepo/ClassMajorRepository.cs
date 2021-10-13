using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.ClassMajorRepo
{
    public class ClassMajorRepository : BaseRepository<ClassMajor>, IClassMajorRepository
    {
        public ClassMajorRepository(DbContext context) : base(context)
        {
        }
    }
}
