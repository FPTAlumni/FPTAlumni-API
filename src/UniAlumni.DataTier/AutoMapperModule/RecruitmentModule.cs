using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Recruitment;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class RecruitmentModule
    {
        public static void ConfigRecruitmentModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Recruitment, RecruitmentViewModel>().ForMember(des => des.Type, opt => opt.MapFrom(
                src => ((RecruitmentEnum.RecruitmentType)src.Type).ToString()));
            mc.CreateMap<RecruitmentCreateRequest, Recruitment>();
            mc.CreateMap<RecruitmentUpdateRequest, Recruitment>();
        }
    }
}
