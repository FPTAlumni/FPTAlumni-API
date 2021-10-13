using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Referral
{
    public class ReferralUpdateRequest : ReferralCreateRequest
    {
        public int Id { get; set; }
        public byte? Status { get; set; }
    }
}
