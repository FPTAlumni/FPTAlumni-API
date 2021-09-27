using Microsoft.Extensions.DependencyInjection;
using UniAlumni.Business.Services;

namespace UniAlumni.Business
{
    public static class ModuleRegister
    {
        /// <summary>
        /// register all Business Layer.
        /// </summary>
        /// <param name="services">service collection from startup.</param>
        public static IServiceCollection RegisterBusinessModule(this IServiceCollection services)
        {
            services.RegisterServicesModule();
            return services;
        }
    }
}