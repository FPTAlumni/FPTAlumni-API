using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Event;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class EventMappingProfiles
    {
        public static IMapperConfigurationExpression ConfigEventModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Event, GetEventDetail>()
                .ForMember(des=>des.GroupName,
                    map=>map.MapFrom(src=>src.Group.GroupName))
                .ReverseMap();
            configuration.CreateMap<Event, CreateEventRequestBody>().ReverseMap();
            configuration.CreateMap<Event, UpdateEventRequestBody>().ReverseMap();

            return configuration;
        }
    }
}