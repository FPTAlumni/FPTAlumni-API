using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class UniversityMajor
    {
        public UniversityMajor()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public int MajorId { get; set; }
        public int UniversityId { get; set; }

        public virtual Major Major { get; set; }
        public virtual University University { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
