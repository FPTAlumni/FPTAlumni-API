using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class MajorUniversity
    {
        public int UniversityId { get; set; }
        public int MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual University University { get; set; }
    }
}
