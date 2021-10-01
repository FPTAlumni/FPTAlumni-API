using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class GroupModule
    {
        public static void ConfigGroupModule (this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Group, GroupViewModel>();
            mc.CreateMap<GroupCreateRequest, Group>();
            mc.CreateMap<GroupUpdateRequest, Group>();
        }
    }
}
