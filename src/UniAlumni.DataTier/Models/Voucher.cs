using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Referrals = new HashSet<Referral>();
        }

        public int Id { get; set; }
        public string RelationshipName { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
