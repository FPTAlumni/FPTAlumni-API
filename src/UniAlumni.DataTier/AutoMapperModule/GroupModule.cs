using AutoMapper;
using System.Linq;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class GroupModule
    {
        public static void ConfigGroupModule (this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Group, GroupViewModel>()
                .ForMember(des => des.NumberOfMembers, opt => opt.MapFrom(
                    src => src.AlumniGroups.Where(ag => ag.Status == (int)AlumniGroupEnum.AlumniGroupStatus.Active)
                    .Count()));
            mc.CreateMap<GroupCreateRequest, Group>();
            mc.CreateMap<GroupUpdateRequest, Group>();
            mc.CreateMap<Group, BaseGroupModel>();
        }
    }
}
