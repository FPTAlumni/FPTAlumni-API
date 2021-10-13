using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.AlumniGroup
{
    public class SearchAlumniGroupModel
    {
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public DateTime? RegisteredFromDate { get; set; } = null;
        public DateTime? RegisteredToDate { get; set; } = null;
        public AlumniGroupEnum.AlumniGroupStatus? Status { get; set; } = null;
    }
}
