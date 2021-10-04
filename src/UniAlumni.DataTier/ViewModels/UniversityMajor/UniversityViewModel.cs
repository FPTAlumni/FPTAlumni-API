using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Major;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.DataTier.ViewModels.UniversityMajor
{
    public class BaseUniversityMajorModel
    {
        public int Id { get; set; }
    }
    public class UniMajorModelGroup : BaseMajorModel
    {
        public BaseUniversityModel University;
        public BaseMajorModel Major;
    }
    public class UniversityMajorViewModel
    {

    }
}
