using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class AlumniGroupEnum
    {
        public enum AlumniGroupStatus
        {
            Inactive,
            Active,
            Pending,
            Banned
        }
        public enum AlumniGroupSortCriteria
        {

            RegisteredDate,


            Status
        }
    }
}
