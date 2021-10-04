using AutoMapper;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.ViewModels.Category;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class CategoryMappingProfiles
    {
        public static IMapperConfigurationExpression ConfigCategoryModule(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, GetCategoryDetail>().ReverseMap();
            configuration.CreateMap<Category, CreateCategoryRequestBody>().ReverseMap();
            configuration.CreateMap<Category, UpdateCategoryRequestBody>().ReverseMap();
            configuration.CreateMap<Category, BaseCategoryModel>().ReverseMap();
            return configuration;
        }
    }
}