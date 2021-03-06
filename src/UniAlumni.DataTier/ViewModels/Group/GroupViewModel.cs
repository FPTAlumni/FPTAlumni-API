using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.Event;
using UniAlumni.DataTier.ViewModels.Major;
using UniAlumni.DataTier.ViewModels.News;
using UniAlumni.DataTier.ViewModels.Recruitment;
using UniAlumni.DataTier.ViewModels.University;
using UniAlumni.DataTier.ViewModels.UniversityMajor;

namespace UniAlumni.DataTier.ViewModels.Group
{
    public class BaseGroupModel 
    { 
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
    public class RecruitmentBaseGroupModel : BaseGroupModel
    {
        public BaseGroupModel ParentGroup { get; set; }
    }
    public class NewsBaseGroupModel : BaseGroupModel
    {
        public string Banner { get; set; }
    }
    public class GroupViewModel : BaseGroupModel
    {
        public string Banner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public string StringStatus { get; set; }
        public BaseAlumniModel GroupLeader { get; set; }
        public BaseUniversityModel University { get; set; }
        public BaseMajorModel Major { get; set; }
        public BaseGroupModel ParentGroup { get; set; }
        public int? NumberOfMembers { get; set; }
    }
    public class GroupRequestViewModel : GroupViewModel
    {
        public string RequestStatus { get; set; }
    }
    public class GroupDetailModel : GroupViewModel
    {
        public ICollection<RecruitmentViewModel> Recruitments;
        public ICollection<NewsViewModel> News;
        public ICollection<GetEventDetail> Events;
    }
}
