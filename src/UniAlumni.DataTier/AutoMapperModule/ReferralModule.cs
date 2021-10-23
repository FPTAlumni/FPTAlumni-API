using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Referral;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class ReferralModule
    {
        public static void ConfigReferralModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Referral, ReferralViewModel>()
                .ForMember(des => des.StringStatus, opt => opt.MapFrom(
                    src => ((ReferralEnum.ReferralStatus)src.Status).ToString()))
                .ForMember(des => des.Major, opt => opt.MapFrom(
                    src => src.Nominator.ClassMajor.Major))
                .ForMember(des => des.University, opt => opt.MapFrom(
                    src => src.Nominator.ClassMajor.Class.University));

            mc.CreateMap<ReferralCreateRequest, Referral>();
            mc.CreateMap<ReferralUpdateRequest, Referral>();
        }

    }
}
