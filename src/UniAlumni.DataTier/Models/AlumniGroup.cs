using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("AlumniGroup")]
    public partial class AlumniGroup
    {
        [Key]
        public int AlumniId { get; set; }
        [Key]
        public int GroupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegisteredDate { get; set; }
        public byte? Status { get; set; }
        public int? UniversityId { get; set; }

        [ForeignKey(nameof(AlumniId))]
        [InverseProperty(nameof(Alumnus.AlumniGroups))]
        public virtual Alumnus Alumni { get; set; }
        [ForeignKey(nameof(GroupId))]
        [InverseProperty("AlumniGroups")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(UniversityId))]
        [InverseProperty("AlumniGroups")]
        public virtual University University { get; set; }
    }
}
