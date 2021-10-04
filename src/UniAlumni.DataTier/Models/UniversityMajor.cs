using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("UniversityMajor")]
    public partial class UniversityMajor
    {
        public UniversityMajor()
        {
            Alumni = new HashSet<Alumnus>();
            Groups = new HashSet<Group>();
        }

        [Key]
        public int Id { get; set; }
        public int MajorId { get; set; }
        public int UniversityId { get; set; }

        [ForeignKey(nameof(MajorId))]
        [InverseProperty("UniversityMajors")]
        public virtual Major Major { get; set; }
        [ForeignKey(nameof(UniversityId))]
        [InverseProperty("UniversityMajors")]
        public virtual University University { get; set; }
        [InverseProperty(nameof(Alumnus.UniversityMajor))]
        public virtual ICollection<Alumnus> Alumni { get; set; }
        [InverseProperty(nameof(Group.UniversityMajor))]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
