using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("EventRegistration")]
    public partial class EventRegistration
    {
        [Key]
        public int AlumniId { get; set; }
        [Key]
        public int EventId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegisteredDate { get; set; }
        public byte? Status { get; set; }

        [ForeignKey(nameof(AlumniId))]
        [InverseProperty(nameof(Alumnus.EventRegistrations))]
        public virtual Alumnus Alumni { get; set; }
        [ForeignKey(nameof(EventId))]
        [InverseProperty("EventRegistrations")]
        public virtual Event Event { get; set; }
    }
}
