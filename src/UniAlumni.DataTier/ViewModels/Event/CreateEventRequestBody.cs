using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using UniAlumni.DataTier.Utility;

namespace UniAlumni.DataTier.ViewModels.Event
{
    public class CreateEventRequestBody
    {
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
        
        public int? GroupId { get; set; }
    }
}