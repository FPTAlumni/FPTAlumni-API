using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Tag")]
    public partial class Tag
    {
        public Tag()
        {
            TagNews = new HashSet<TagNews>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Tagname { get; set; }
        public byte? Status { get; set; }

        [InverseProperty("Tag")]
        public virtual ICollection<TagNews> TagNews { get; set; }
    }
}
