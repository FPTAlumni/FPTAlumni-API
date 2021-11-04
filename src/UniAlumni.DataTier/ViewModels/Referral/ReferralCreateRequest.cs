using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Referral
{
    public class ReferralCreateRequest
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int? NominatorId { get; set; }
        public int? VoucherId { get; set; }
        public int? Year { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string HighSchoolName { get; set; }
    }
}
