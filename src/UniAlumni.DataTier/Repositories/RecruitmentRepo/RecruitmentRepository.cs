using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.RecruitmentRepo
{
    public class RecruitmentRepository:BaseRepository<Recruitment>, IRecruitmentRepository
    {
        public RecruitmentRepository(DbContext context) : base(context)
        {
        }
    }
}
