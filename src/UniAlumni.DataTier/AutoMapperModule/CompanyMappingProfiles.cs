using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Company;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class CompanyMappingProfiles
    {
        public static IMapperConfigurationExpression ConfigCompanyModule(
          this  IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Company, GetCompanyDetail>().ReverseMap();
            configuration.CreateMap<Company, CreateCompanyRequestBody>().ReverseMap();
            configuration.CreateMap<Company, UpdateCompanyRequestBody>().ReverseMap();
            configuration.CreateMap<Company, BaseCompanyModel>();
            configuration.CreateMap<Company, RecruitmentCompanyModel>();
            return configuration;
        }
    }
}