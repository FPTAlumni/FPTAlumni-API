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
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
