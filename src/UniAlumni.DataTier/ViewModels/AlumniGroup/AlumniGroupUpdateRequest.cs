using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.AlumniGroup
{
    public class AlumniGroupUpdateRequest
    {
        public int AlumniId { get; set; }
        public int GroupId { get; set; }
        public byte Status { get; set; }
    }
}
