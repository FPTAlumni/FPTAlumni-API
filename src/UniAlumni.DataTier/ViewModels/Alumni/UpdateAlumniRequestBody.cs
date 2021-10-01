using System;
using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class UpdateAlumniRequestBody
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MinLength(9)]
        [MaxLength(13)]
        public string Phone { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [MaxLength(200)]
        public string Address { get; set; }
        
        public DateTime DoB { get; set; }
        public string Job { get; set; }
        public string AboutMe { get; set; }
        
        [Required]
        public int? UniversityId { get; set; }
        
        [Required]
        public int? CompanyId { get; set; }
    }
}