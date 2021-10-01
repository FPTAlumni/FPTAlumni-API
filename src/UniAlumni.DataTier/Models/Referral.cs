using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Referral
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string VoucherCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? NominatorId { get; set; }
        public int? VoucherId { get; set; }

        public virtual Alumnus Nominator { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
