using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.ReferralRepo
{
    public class ReferralRepository : BaseRepository<Referral>, IReferralRepository
    {
        public ReferralRepository(DbContext context) : base(context)
        {
        }
    }
}
