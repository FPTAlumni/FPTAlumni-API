using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UniAlumni.DataTier.Utility;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class UpdateAlumniRequestBody
    {
        [Required]
        public int Id { get; set;}
        
        [MinLength(9)]
        [MaxLength(13)]
        public string Phone { get; set; }
        
        [MinLength((6))]
        [MaxLength(50)]
        public string FullName { get; set; }
        
        [MaxLength(200)]
        public string Address { get; set; }
        
        [JsonProperty("dob")]
        public DateTime DoB { get; set; }
        
        
        public string Job { get; set; }
        
        [MaxLength(200)]
        public string AboutMe { get; set; }
        
        public int? CompanyId { get; set; }
        
        [Required]
        public int MajorId { get; set; }
        [Required]
        public int ClassId { get; set; }
    }
}