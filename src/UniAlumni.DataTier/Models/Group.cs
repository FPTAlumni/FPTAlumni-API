using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Group")]
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

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }
        [StringLength(200)]
        public string Banner { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupLeaderId { get; set; }
        public int? MajorId { get; set; }
        public int? ParentGroupId { get; set; }

        [ForeignKey(nameof(GroupLeaderId))]
        [InverseProperty(nameof(Alumnus.Groups))]
        public virtual Alumnus GroupLeader { get; set; }
        [ForeignKey(nameof(MajorId))]
        [InverseProperty("Groups")]
        public virtual Major Major { get; set; }
        [ForeignKey(nameof(ParentGroupId))]
        [InverseProperty(nameof(Group.InverseParentGroup))]
        public virtual Group ParentGroup { get; set; }
        [InverseProperty(nameof(AlumniGroup.Group))]
        public virtual ICollection<AlumniGroup> AlumniGroups { get; set; }
        [InverseProperty(nameof(Event.Group))]
        public virtual ICollection<Event> Events { get; set; }
        [InverseProperty(nameof(Group.ParentGroup))]
        public virtual ICollection<Group> InverseParentGroup { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<News> News { get; set; }
        [InverseProperty(nameof(Recruitment.GroupOrigin))]
        public virtual ICollection<Recruitment> RecruitmentGroupOrigins { get; set; }
        [InverseProperty(nameof(Recruitment.Group))]
        public virtual ICollection<Recruitment> RecruitmentGroups { get; set; }
    }
}
