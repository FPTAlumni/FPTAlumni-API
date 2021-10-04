using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.University
{
    public class BaseUniversityModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
   public class UniversityViewModel : BaseUniversityModel
    {
        public string Address { get; set; }
        public string Logo { get; set; }
    }
}
