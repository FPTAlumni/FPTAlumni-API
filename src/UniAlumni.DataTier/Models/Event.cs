using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Event")]
    public partial class Event
    {
        public Event()
        {
            EventRegistrations = new HashSet<EventRegistration>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string EventName { get; set; }
        [Required]
        public string EventContent { get; set; }
        [StringLength(200)]
        public string Banner { get; set; }
        [Required]
        [StringLength(150)]
        public string Location { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationEndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("Events")]
        public virtual Group Group { get; set; }
        [InverseProperty(nameof(EventRegistration.Event))]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }
    }
}
