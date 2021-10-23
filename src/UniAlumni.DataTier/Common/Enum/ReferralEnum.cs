using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class ReferralEnum
    {
        public enum ReferralStatus
        {
            Inactive,
            Verified,
            Paid,
            Pending,
            Rejected
        }
        public enum ReferralSortCriteria
        {

            CreatedDate,


            Status
        }
    }
}
