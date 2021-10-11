using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.AlumniGroup;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class AlumniGroupMappingProfiles
    {
        public static IMapperConfigurationExpression ConfigAlumniGroupModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<AlumniGroup, GetAlumniGroupDetail>().ReverseMap();
         

            return configuration;
        }
    }
}