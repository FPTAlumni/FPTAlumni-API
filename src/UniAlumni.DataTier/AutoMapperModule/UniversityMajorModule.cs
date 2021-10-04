using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.UniversityMajor;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class UniversityMajorModule
    {
        public static void ConfigUniversityMajorModule(this IMapperConfigurationExpression mc)
        {
            //mc.CreateMap<UniversityMajor, BaseUniversityMajorModel>();
            mc.CreateMap<UniversityMajor, UniMajorModelGroup>();
            
        }
    }
}
