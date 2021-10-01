using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Event
    {
        public Event()
        {
            EventRegistrations = new HashSet<EventRegistration>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventContent { get; set; }
        public string Banner { get; set; }
        public string Location { get; set; }
        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }
    }
}
