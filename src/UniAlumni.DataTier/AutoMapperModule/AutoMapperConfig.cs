using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace UniAlumni.DataTier.AutoMapperModule
{
    public static class AutoMapperConfig
    {

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.ConfigGroupModule();   
                mc.ConfigAlumniModule();
                mc.ConfigCategoryModule();
                mc.ConfigMajorModule();
                mc.ConfigCompanyModule();
                mc.ConfigUniversityModule();
                mc.ConfigUniversityMajorModule();
                mc.ConfigNewsModule();
                mc.ConfigTagModule();
                mc.ConfigClassModule();
                mc.ConfigEventModule();
                mc.ConfigAlumniGroupModule();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
