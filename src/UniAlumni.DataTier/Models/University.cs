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
            Classes = new HashSet<Class>();
            Groups = new HashSet<Group>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Logo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }

        [InverseProperty(nameof(Class.University))]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty(nameof(Group.University))]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
