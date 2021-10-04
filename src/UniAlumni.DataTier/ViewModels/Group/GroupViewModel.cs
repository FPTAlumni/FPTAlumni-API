using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.Major;
using UniAlumni.DataTier.ViewModels.University;
using UniAlumni.DataTier.ViewModels.UniversityMajor;

namespace UniAlumni.DataTier.ViewModels.Group
{
    public class BaseGroupModel 
    { 
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
    public class GroupViewModel : BaseGroupModel
    {
        public string Banner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public BaseAlumniModel GroupLeader { get; set; }
        public UniMajorModelGroup UniversityMajor { get; set; }
        public BaseGroupModel ParentGroup { get; set; }
        public int? NumberOfMembers { get; set; }
    }
}
