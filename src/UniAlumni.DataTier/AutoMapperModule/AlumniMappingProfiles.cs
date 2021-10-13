using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class AlumniMappingProfiles 
    {
        public static IMapperConfigurationExpression ConfigAlumniModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Alumnus, GetAlumniDetail>().ReverseMap();
            configuration.CreateMap<Alumnus, CreateAlumniRequestBody>().ReverseMap();
            configuration.CreateMap<Alumnus, UpdateAlumniRequestBody>().ReverseMap();
            configuration.CreateMap<Alumnus, ActivateAlumniRequestBody>().ReverseMap();
            configuration.CreateMap<Alumnus, BaseAlumniModel>();
            configuration.CreateMap<Alumnus, AlumniGroupAlumniModel>();
               
            return configuration;
        }
    }
}