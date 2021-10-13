using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Voucher
{
    public class VoucherCreateRequest
    {
        public decimal DiscountValue { get; set; }
        public int MajorId { get; set; }
        public string RelationshipName { get; set; }
    }
}
