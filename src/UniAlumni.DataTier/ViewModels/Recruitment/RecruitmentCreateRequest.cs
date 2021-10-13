using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Recruitment
{
    public class RecruitmentCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(70)]
        public string Position { get; set; }
        [StringLength(70)]
        public string ExperienceLevel { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [StringLength(320)]
        public string Email { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AlumniId { get; set; }
        public int? GroupId { get; set; }
        public int? CompanyId { get; set; }
        public int? GroupOriginId { get; set; }
        public int? Type { get; set; }
    }
}
