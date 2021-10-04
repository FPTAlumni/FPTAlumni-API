using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class News
    {
        public News()
        {
            TagNews = new HashSet<TagNews>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Banner { get; set; }
        [Required]
        [StringLength(70)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupId { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("News")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(GroupId))]
        [InverseProperty("News")]
        public virtual Group Group { get; set; }
        [InverseProperty("News")]
        public virtual ICollection<TagNews> TagNews { get; set; }
    }
}
