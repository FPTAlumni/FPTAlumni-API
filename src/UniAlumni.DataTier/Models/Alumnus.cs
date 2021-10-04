using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Alumnus
    {
        public Alumnus()
        {
            AlumniGroups = new HashSet<AlumniGroup>();
            EventRegistrations = new HashSet<EventRegistration>();
            Groups = new HashSet<Group>();
            Recruitments = new HashSet<Recruitment>();
            Referrals = new HashSet<Referral>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Uid { get; set; }
        public DateTime DoB { get; set; }
        public string Job { get; set; }
        public string AboutMe { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int MajorId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Major Major { get; set; }
        public virtual ICollection<AlumniGroup> AlumniGroups { get; set; }
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Recruitment> Recruitments { get; set; }
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
