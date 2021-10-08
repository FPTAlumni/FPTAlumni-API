using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class RecruitmentEnum
    {
        public enum RecruitmentStatus
        {
            Inactive,
            Active,
            Pending,
            Rejected
        }
        public enum RecruitmentType
        {
            Fulltime,
            Parttime
        }
        public enum RecruitmentSortCriteria
        {
            Title,
            CreatedDate,
            Status
        }
    }
}
