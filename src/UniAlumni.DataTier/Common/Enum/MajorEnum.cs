using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class MajorEnum
    {
        public enum MajorStatus
        {
            Inactive,
            Active
        }
        public enum MajorSortCriteria
        {

            ShortName,


            CreatedDate,


            Status
        }
    }
}
