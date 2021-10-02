using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.MajorRepo
{
    public class MajorRepository : BaseRepository<Major>, IMajorRepository
    {
        public MajorRepository(DbContext context) : base(context)
        {
        }
    }
}
