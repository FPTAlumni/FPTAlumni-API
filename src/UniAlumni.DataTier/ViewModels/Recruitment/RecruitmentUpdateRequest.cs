using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Recruitment
{
    public class RecruitmentUpdateRequest: RecruitmentCreateRequest
    {
        public int Id { get; set; }
        public int? Status { get; set; }
    }
}
