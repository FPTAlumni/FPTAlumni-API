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
        
        
        [JsonProperty("dob")]
        public DateTime DoB { get; set; }

        [Required]
        public int MajorId { get; set; }
        [Required]
        public int ClassId { get; set; }
    }
}