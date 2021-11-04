using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Index(nameof(Phone), Name = "UQ__Alumni__5C7E359E27655E6B", IsUnique = true)]
    [Index(nameof(Email), Name = "UQ__Alumni__A9D10534D1C935CC", IsUnique = true)]
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

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(320)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [StringLength(70)]
        public string FullName { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [Required]
        [StringLength(256)]
        public string Uid { get; set; }
        [Column(TypeName = "date")]
        public DateTime DoB { get; set; }
        [StringLength(70)]
        public string Job { get; set; }
        [StringLength(500)]
        public string AboutMe { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? CompanyId { get; set; }
        public int? ClassMajorId { get; set; }
        public string Image { get; set; }

        [ForeignKey(nameof(ClassMajorId))]
        [InverseProperty("Alumni")]
        public virtual ClassMajor ClassMajor { get; set; }
        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Alumni")]
        public virtual Company Company { get; set; }
        [InverseProperty(nameof(AlumniGroup.Alumni))]
        public virtual ICollection<AlumniGroup> AlumniGroups { get; set; }
        [InverseProperty(nameof(EventRegistration.Alumni))]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }
        [InverseProperty(nameof(Group.GroupLeader))]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty(nameof(Recruitment.Alumni))]
        public virtual ICollection<Recruitment> Recruitments { get; set; }
        [InverseProperty(nameof(Referral.Nominator))]
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
