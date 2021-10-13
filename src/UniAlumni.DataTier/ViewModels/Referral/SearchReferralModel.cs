using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Referral
{
    public class SearchReferralModel
    {
        public int? NominatorId { get; set; } = null;
        public int? MajorId { get; set; } = null;
        public ReferralEnum.ReferralStatus? Status { get; set; } = null;
    }
}
