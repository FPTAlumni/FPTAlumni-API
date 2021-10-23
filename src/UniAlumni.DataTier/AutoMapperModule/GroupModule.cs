using AutoMapper;
using System.Linq;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Group;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class GroupModule
    {
        public static void ConfigGroupModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Group, GroupViewModel>()
                .ForMember(des => des.NumberOfMembers, opt => opt.MapFrom(
                    src => src.AlumniGroups.Where(ag => ag.Status == (int)AlumniGroupEnum.AlumniGroupStatus.Active)
                    .Count()))
                .ForMember(des => des.StringStatus, opt => opt.MapFrom(
                        src => ((GroupEnum.GroupStatus)src.Status).ToString()));
            mc.CreateMap<GroupCreateRequest, Group>();
            mc.CreateMap<GroupUpdateRequest, Group>();
            mc.CreateMap<Group, BaseGroupModel>();
            mc.CreateMap<Group, NewsBaseGroupModel>();
            mc.CreateMap<Group, GroupDetailModel>()
                .ForMember(des => des.NumberOfMembers, opt => opt.MapFrom(
                    src => src.AlumniGroups.Where(ag => ag.Status == (int)AlumniGroupEnum.AlumniGroupStatus.Active)
                    .Count()))
                .ForMember(des => des.StringStatus, opt => opt.MapFrom(
                        src => ((GroupEnum.GroupStatus)src.Status).ToString()))
                .ForMember(des => des.Recruitments, opt => opt.MapFrom(
                    src => src.RecruitmentGroupOrigins.Concat(src.RecruitmentGroups)));
            mc.CreateMap<Group, RecruitmentBaseGroupModel>();
            mc.CreateMap<Group, GroupRequestViewModel>()
                .ForMember(des => des.NumberOfMembers, opt => opt.MapFrom(
                    src => src.AlumniGroups.Where(ag => ag.Status == (int)AlumniGroupEnum.AlumniGroupStatus.Active)
                    .Count()))
                .ForMember(des => des.StringStatus, opt => opt.MapFrom(
                        src => ((GroupEnum.GroupStatus)src.Status).ToString()))
                .ForMember(des => des.RequestStatus, opt => opt.Ignore());
        }
    }
}
