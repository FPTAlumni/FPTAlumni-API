using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Enum
{
    public class VoucherEnum
    {
        public enum VoucherStatus
        {
            Inactive,
            Active
        }
        public enum VoucherSortCriteria
        {
            CreatedDate,
            Status
        }
    }
}
