using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class NewsEnum
    {
        public enum NewsStatus
        {
            Inactive,
            Active
        }
        public enum NewsSortCriteria
        {
            Title,
            CreatedDate,
            Status
        }
    }
}
