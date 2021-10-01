using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Company
    {
        public Company()
        {
            Alumni = new HashSet<Alumnus>();
            Recruitments = new HashSet<Recruitment>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Business { get; set; }
        public string Description { get; set; }
        public int? CreateBy { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<Alumnus> Alumni { get; set; }
        public virtual ICollection<Recruitment> Recruitments { get; set; }
    }
}
