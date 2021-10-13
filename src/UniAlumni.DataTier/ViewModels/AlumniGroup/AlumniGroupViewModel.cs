using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.DataTier.ViewModels.AlumniGroup
{
    public class AlumniGroupViewModel
    {
        public AlumniGroupAlumniModel Alumni { get; set; }
        public int? GroupId { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public string Status { get; set; }
    }
}
