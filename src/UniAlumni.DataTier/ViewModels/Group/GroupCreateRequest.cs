using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Group
{
    public class GroupCreateRequest
    {
     
        [StringLength(100)]
        public string GroupName { get; set; }
        [StringLength(200)]
        public string Banner { get; set; }
        public int? GroupLeaderId { get; set; }
        public int? UniversityId { get; set; }
        public int? MajorId { get; set; }
        public int? ParentGroupId { get; set; }
    }
}
