using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UniAlumni.DataTier.Utility;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class CreateAlumniRequestBody
    {
        [Required]
        public string Uid { get; set; }
        
        [Required]
        [MinLength(9)]
        [MaxLength(13)]
        public string Phone { get; set; }
        
        [Required]
        [MinLength((6))]
        [MaxLength(50)]
        public string FullName { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        
        [JsonProperty("dob")]
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime DoB { get; set; }
        
        
        public string Job { get; set; }
        
        [MaxLength(200)]
        public string AboutMe { get; set; }
        
        [Required]
        public int? CompanyId { get; set; }
        
        public int UniversityMajorId { get; set; }
        
        public int? ClassId { get; set; }
    }
}