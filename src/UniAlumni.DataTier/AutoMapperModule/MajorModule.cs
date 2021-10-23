using AutoMapper;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class MajorModule
    {
        public static void ConfigMajorModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Major, MajorViewModel>()
                .ForMember(des => des.StringStatus, opt => opt.MapFrom(
                        src => ((MajorEnum.MajorStatus)src.Status).ToString()));
            mc.CreateMap<MajorCreateRequest, Major>();
            mc.CreateMap<MajorUpdateRequest, Major>();
            mc.CreateMap<Major, BaseMajorModel>();
        }
    }
}
