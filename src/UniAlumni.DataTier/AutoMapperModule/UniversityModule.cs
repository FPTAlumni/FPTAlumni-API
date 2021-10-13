using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class UniversityModule
    {
        public static void ConfigUniversityModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<University, BaseUniversityModel>();
            mc.CreateMap<University, UniversityViewModel>()
                .ForMember(des=>des.Classes,
                    map=> map.MapFrom(src=>src.Classes))
                .ReverseMap();
            mc.CreateMap<University, CreateUniversityRequestBody>().ReverseMap();
            mc.CreateMap<University, UpdateUniversityRequestBody>().ReverseMap();
        }
    }
}
