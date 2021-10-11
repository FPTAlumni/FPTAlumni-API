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
            Alumni = new HashSet<Alumnus>();
            Groups = new HashSet<Group>();
            UniversityMajors = new HashSet<UniversityMajor>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string ClassOf { get; set; }
        public int? StartYear { get; set; }

        [InverseProperty(nameof(Alumnus.Class))]
        public virtual ICollection<Alumnus> Alumni { get; set; }
        [InverseProperty(nameof(Group.Class))]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty(nameof(UniversityMajor.Class))]
        public virtual ICollection<UniversityMajor> UniversityMajors { get; set; }
    }
}
