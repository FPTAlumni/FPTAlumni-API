using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class AlumniMappingProfiles 
    {
        public static IMapperConfigurationExpression ConfigAlumniModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Alumnus, GetAlumniDetail>().ReverseMap()
                .ForMember(des => des.Status,
                    map => map.MapFrom(src => src.Status));
            return configuration;
        }
    }
}