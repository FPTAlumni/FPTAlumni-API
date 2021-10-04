using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class University
    {
        public University()
        {
            UniversityMajors = new HashSet<UniversityMajor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<UniversityMajor> UniversityMajors { get; set; }
    }
}
