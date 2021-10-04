using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.University;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class UniversityModule
    {
        public static void ConfigUniversityModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<University, BaseUniversityModel>();
            mc.CreateMap<University, UniversityViewModel>().ReverseMap();
        }
    }
}
