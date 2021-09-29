using System;
using System.ComponentModel.DataAnnotations;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class CreateAlumniRequestBody
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        
        [Required]
        public string Uid { get; set; }
        public DateTime DoB { get; set; }
        public string Job { get; set; }
        public string AboutMe { get; set; }
        public byte? Status { get; set; } 
        public int? UniversityId { get; set; }
        public int? CompanyId { get; set; }
    }
}