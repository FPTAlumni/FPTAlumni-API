using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class RecruitmentEnum
    {
        public enum RecruitmentStatus
        {
            Inactive = 0,
            Active = 1,
            Pending = 2,
            Rejected = 3
        }
        public enum RecruitmentType
        {
            [Display(Name = "Full-time")]
            Fulltime,
            [Display(Name = "Part-time")]
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
