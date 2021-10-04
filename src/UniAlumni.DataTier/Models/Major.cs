using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Major
    {
        public Major()
        {
            Alumni = new HashSet<Alumnus>();
            UniversityMajors = new HashSet<UniversityMajor>();
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string VietnameseName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<Alumnus> Alumni { get; set; }
        public virtual ICollection<UniversityMajor> UniversityMajors { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
