using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.University;
using UniAlumni.DataTier.ViewModels.UniversityMajor;

namespace UniAlumni.DataTier.ViewModels.Major
{
    public class BaseMajorModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class MajorViewModel : BaseMajorModel
    {
        public string ShortName { get; set; }        
        public string VietnameseName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }     
        public string StringStatus { get; set; }
    }
}
