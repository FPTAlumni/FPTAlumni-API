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
            /// <summary>
            /// Fullname attr
            /// </summary>
            ShortName,

            /// <summary>
            /// CreateDate attr
            /// </summary>
            CreatedDate,

            /// <summary>
            /// Status attr
            /// </summary>
            Status
        }
    }
}
