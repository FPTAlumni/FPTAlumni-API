using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels
{
    public class GroupViewModel
    {
        public string GroupName { get; set; }
        public string Banner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupLeaderId { get; set; }
        public int? MajorId { get; set; }
        public int? ParentGroupId { get; set; }
    }
}
