using System;
using AutoMapper;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Alumni;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class AlumniMappingProfiles 
    {
        public static IMapperConfigurationExpression ConfigAlumniModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Alumnus, GetAlumniDetail>()
                .ForMember(des =>des.Status,
                    map => 
                        map.MapFrom(src=>Enum.GetName(typeof(AlumniEnum.AlumniStatus), src.Status)))
                .ForMember(des=>des.Company,
                map=>map.MapFrom(src=>src.Company))
                .ForMember(des=>des.Major,
                map=>map.MapFrom(src=>src.ClassMajor.Major))
                .ForMember(des=>des.University,
                    map=>map.MapFrom(src=>src.ClassMajor.Class.University))
                .ForMember(des=>des.Class,
                    map=>map.MapFrom(src=> src.ClassMajor.Class))
                .ReverseMap();
            configuration.CreateMap<Alumnus, CreateAlumniRequestBody>().ReverseMap();
            configuration.CreateMap<Alumnus, UpdateAlumniRequestBody>().ReverseMap();
            configuration.CreateMap<Alumnus, ActivateAlumniRequestBody>().ReverseMap();
            configuration.CreateMap<Alumnus, BaseAlumniModel>();
            configuration.CreateMap<Alumnus, AlumniGroupAlumniModel>();
               
            return configuration;
        }
    }
}