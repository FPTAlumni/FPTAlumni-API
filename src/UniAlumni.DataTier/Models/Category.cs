using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            News = new HashSet<News>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string CategoryName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public byte? Status { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<News> News { get; set; }
    }
}
