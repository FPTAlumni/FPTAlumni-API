using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Major;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class MajorModule
    {
        public static void ConfigMajorModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Major, MajorViewModel>();
            mc.CreateMap<MajorCreateRequest, Major>();
            mc.CreateMap<MajorUpdateRequest, Major>();
        }
    }
}
