using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Voucher")]
    public partial class Voucher
    {
        public Voucher()
        {
            Referrals = new HashSet<Referral>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RelationshipName { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal DiscountValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? MajorId { get; set; }

        [ForeignKey(nameof(MajorId))]
        [InverseProperty("Vouchers")]
        public virtual Major Major { get; set; }
        [InverseProperty(nameof(Referral.Voucher))]
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
