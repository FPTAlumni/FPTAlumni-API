﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Referral")]
    public partial class Referral
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string FullName { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(15)]
        public string VoucherCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? NominatorId { get; set; }
        public int? VoucherId { get; set; }
        public int? UniversityMajorId { get; set; }

        [ForeignKey(nameof(NominatorId))]
        [InverseProperty(nameof(Alumnus.Referrals))]
        public virtual Alumnus Nominator { get; set; }
        [ForeignKey(nameof(UniversityMajorId))]
        [InverseProperty("Referrals")]
        public virtual UniversityMajor UniversityMajor { get; set; }
        [ForeignKey(nameof(VoucherId))]
        [InverseProperty("Referrals")]
        public virtual Voucher Voucher { get; set; }
    }
}
