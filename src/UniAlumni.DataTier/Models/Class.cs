using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Class")]
    public partial class Class
    {
        public Class()
        {
            ClassMajors = new HashSet<ClassMajor>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string ClassOf { get; set; }
        public int? StartYear { get; set; }
        public int? UniversityId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? Status { get; set; }

        [ForeignKey(nameof(UniversityId))]
        [InverseProperty("Classes")]
        public virtual University University { get; set; }
        [InverseProperty(nameof(ClassMajor.Class))]
        public virtual ICollection<ClassMajor> ClassMajors { get; set; }
    }
}
