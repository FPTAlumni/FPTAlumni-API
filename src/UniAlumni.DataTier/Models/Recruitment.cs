using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Recruitment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string ExperienceLevel { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? AlumniId { get; set; }
        public int? GroupId { get; set; }
        public int? CompanyId { get; set; }
        public int? GroupOriginId { get; set; }

        public virtual Alumnus Alumni { get; set; }
        public virtual Company Company { get; set; }
        public virtual Group Group { get; set; }
        public virtual Group GroupOrigin { get; set; }
    }
}
