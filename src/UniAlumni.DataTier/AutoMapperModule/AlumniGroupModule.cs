using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.AlumniGroup;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class AlumniGroupModule
    {
        public static void ConfigAlumniGroupModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<AlumniGroup, AlumniGroupViewModel>()
                .ForMember(des => des.Status, opt => opt.MapFrom(
                    src => ((AlumniGroupEnum.AlumniGroupStatus)src.Status).ToString()));
        }
    }
}
