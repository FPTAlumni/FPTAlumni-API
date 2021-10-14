using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class TagModule
    {
        public static void ConfigTagModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<TagCreateRequest, Tag>().ReverseMap();
            mc.CreateMap<Tag, TagViewModel>();
        }
    }
}
