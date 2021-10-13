using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Voucher
{
    public class SearchVoucherModel
    {
        public int? MajorId { get; set; } = null;

        public VoucherEnum.VoucherStatus? Status { get; set; } = null;
    }
}
