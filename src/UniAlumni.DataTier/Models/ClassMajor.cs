using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("ClassMajor")]
    public partial class ClassMajor
    {
        public ClassMajor()
        {
            Alumni = new HashSet<Alumnus>();
        }

        [Key]
        public int Id { get; set; }
        public int? ClassId { get; set; }
        public int? MajorId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty("ClassMajors")]
        public virtual Class Class { get; set; }
        [ForeignKey(nameof(MajorId))]
        [InverseProperty("ClassMajors")]
        public virtual Major Major { get; set; }
        [InverseProperty(nameof(Alumnus.ClassMajor))]
        public virtual ICollection<Alumnus> Alumni { get; set; }
    }
}
