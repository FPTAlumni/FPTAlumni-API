using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Voucher
{
    public class VoucherUpdateRequest : VoucherCreateRequest
    {
        public int Id { get; set; }
        public byte? Status { get; set; }
    }
}
