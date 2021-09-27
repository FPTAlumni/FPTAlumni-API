using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Request.Group;
using UniAlumni.DataTier.ViewModels;

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
