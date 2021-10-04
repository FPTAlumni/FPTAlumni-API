using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Group
{
    public class GroupUpdateRequest
    {
        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }
        [StringLength(200)]
        public string Banner { get; set; }
        public byte? Status { get; set; }
        public int? GroupLeaderId { get; set; }
        public int? UniversityMajorId { get; set; }
        public int? ParentGroupId { get; set; }
    }
}
