using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class TagEnum
    {
        public enum TagStatus
        {
            Inactive,
            Active,
            Banned
        }
        public enum TagSortCriteria
        {
            TagName,
            Status
        }
    }
}
