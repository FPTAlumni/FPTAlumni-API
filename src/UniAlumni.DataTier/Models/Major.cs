using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Major")]
    public partial class Major
    {
        public Major()
        {
            ClassMajors = new HashSet<ClassMajor>();
            Groups = new HashSet<Group>();
            Vouchers = new HashSet<Voucher>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(5)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string VietnameseName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }

        [InverseProperty(nameof(ClassMajor.Major))]
        public virtual ICollection<ClassMajor> ClassMajors { get; set; }
        [InverseProperty(nameof(Group.Major))]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty(nameof(Voucher.Major))]
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
