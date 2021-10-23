using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.DataTier.ViewModels.Voucher
{
    public class BaseVoucherModel
    {
        public int Id { get; set; }
        public decimal DiscountValue { get; set; }
        public BaseMajorModel Major { get; set; }
        public string RelationshipName { get; set; }
    }
    public class VoucherViewModel : BaseVoucherModel
    {
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public string StringStatus { get; set; }
    }
}
