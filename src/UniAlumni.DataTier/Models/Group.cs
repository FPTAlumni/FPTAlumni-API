using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Group
    {
        public Group()
        {
            AlumniGroups = new HashSet<AlumniGroup>();
            Events = new HashSet<Event>();
            InverseParentGroup = new HashSet<Group>();
            News = new HashSet<News>();
            RecruitmentGroupOrigins = new HashSet<Recruitment>();
            RecruitmentGroups = new HashSet<Recruitment>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Banner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupLeaderId { get; set; }
        public int? UniversityMajorId { get; set; }
        public int? ParentGroupId { get; set; }

        public virtual Alumnus GroupLeader { get; set; }
        public virtual Group ParentGroup { get; set; }
        public virtual UniversityMajor UniversityMajor { get; set; }
        public virtual ICollection<AlumniGroup> AlumniGroups { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Group> InverseParentGroup { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Recruitment> RecruitmentGroupOrigins { get; set; }
        public virtual ICollection<Recruitment> RecruitmentGroups { get; set; }
    }
}
