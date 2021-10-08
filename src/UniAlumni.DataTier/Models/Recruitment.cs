using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Recruitment")]
    public partial class Recruitment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(70)]
        public string Position { get; set; }
        [StringLength(70)]
        public string ExperienceLevel { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [StringLength(320)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? AlumniId { get; set; }
        public int? GroupId { get; set; }
        public int? CompanyId { get; set; }
        public int? GroupOriginId { get; set; }
        public int? Type { get; set; }

        [ForeignKey(nameof(AlumniId))]
        [InverseProperty(nameof(Alumnus.Recruitments))]
        public virtual Alumnus Alumni { get; set; }
        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Recruitments")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(GroupId))]
        [InverseProperty("RecruitmentGroups")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(GroupOriginId))]
        [InverseProperty("RecruitmentGroupOrigins")]
        public virtual Group GroupOrigin { get; set; }
    }
}
