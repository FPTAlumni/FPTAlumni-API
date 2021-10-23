using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.Company;
using UniAlumni.DataTier.ViewModels.Group;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.DataTier.ViewModels.Recruitment
{
    public class RecruitmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string ExperienceLevel { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public BaseAlumniModel Alumni { get; set; }
        public BaseGroupModel Group { get; set; }
        public RecruitmentCompanyModel Company { get; set; }
        public RecruitmentBaseGroupModel GroupOrigin { get; set; }
        public BaseMajorModel Major { get; set; }
        public string StringType { get; set; }
        public int? Type { get; set; }
        public string StringStatus { get; set; }
        public byte? Status { get; set; }
    }
}
