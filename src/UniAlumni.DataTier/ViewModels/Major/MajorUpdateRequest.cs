using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Major
{
    public class MajorUpdateRequest
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string VietnameseName { get; set; }
        public byte? Status { get; set; }
    }
}
