using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Recruitment
{
    public class SearchRecruitmentModel
    {
        public int? CompanyId { get; set; } = null;
        public int? MajorId { get; set; } = null;
        public int? GroupId { get; set; } = null;

        [DefaultValue(RecruitmentEnum.RecruitmentType.Fulltime)]
        public RecruitmentEnum.RecruitmentType? Type { get; set; } = RecruitmentEnum.RecruitmentType.Fulltime;

        [DefaultValue(RecruitmentEnum.RecruitmentStatus.Active)]
        public RecruitmentEnum.RecruitmentStatus? Status { get; set; } = RecruitmentEnum.RecruitmentStatus.Active;
    }
}
