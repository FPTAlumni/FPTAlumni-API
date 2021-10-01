using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Major
    {
        public Major()
        {
            Groups = new HashSet<Group>();
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string VietnameseName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
