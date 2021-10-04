using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("University")]
    public partial class University
    {
        public University()
        {
            UniversityMajors = new HashSet<UniversityMajor>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Logo { get; set; }

        [InverseProperty(nameof(UniversityMajor.University))]
        public virtual ICollection<UniversityMajor> UniversityMajors { get; set; }
    }
}
