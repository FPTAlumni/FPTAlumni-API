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
            Alumni = new HashSet<Alumnus>();
            AlumniGroups = new HashSet<AlumniGroup>();
            Majors = new HashSet<Major>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Logo { get; set; }

        [InverseProperty(nameof(Alumnus.University))]
        public virtual ICollection<Alumnus> Alumni { get; set; }
        [InverseProperty(nameof(AlumniGroup.University))]
        public virtual ICollection<AlumniGroup> AlumniGroups { get; set; }
        [InverseProperty(nameof(Major.University))]
        public virtual ICollection<Major> Majors { get; set; }
    }
}
