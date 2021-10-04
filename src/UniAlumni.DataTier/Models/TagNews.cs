using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class TagNews
    {
        [Key]
        public int NewsId { get; set; }
        [Key]
        public int TagId { get; set; }

        [ForeignKey(nameof(NewsId))]
        [InverseProperty("TagNews")]
        public virtual News News { get; set; }
        [ForeignKey(nameof(TagId))]
        [InverseProperty("TagNews")]
        public virtual Tag Tag { get; set; }
    }
}
