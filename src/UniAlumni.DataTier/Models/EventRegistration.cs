using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class EventRegistration
    {
        public int AlumniId { get; set; }
        public int EventId { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public byte? Status { get; set; }

        public virtual Alumnus Alumni { get; set; }
        public virtual Event Event { get; set; }
    }
}
