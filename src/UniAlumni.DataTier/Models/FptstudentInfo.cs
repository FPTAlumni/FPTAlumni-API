using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class FptstudentInfo
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string EduMail { get; set; }
        public string Major { get; set; }
        public string Class { get; set; }
    }
}
