using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Class;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class ClassMappingProfiles
    {
        public static IMapperConfigurationExpression ConfigClassModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Class, GetClassDetail>()
                .ForMember(des=>des.UniversityName,
                    map=>map.MapFrom(src=>src.University.Name))
                .ReverseMap();
            configuration.CreateMap<Class, CreateClassRequestBody>().ReverseMap();
            configuration.CreateMap<Class, UpdateClassRequestBody>().ReverseMap();

            return configuration;
        }
    }
}