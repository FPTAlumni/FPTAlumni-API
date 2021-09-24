using Microsoft.Extensions.DependencyInjection;
using UniAlumni.DataTier.Repositories;

namespace UniAlumni.DataTier
{
    /// <summary>
    /// Inject all service related in DataTier.
    /// </summary>
    public static class ModuleRegister
    {
        /// <summary>
        /// register all DataTier Layer.
        /// </summary>
        /// <param name="services">service collection from startup.</param>
        public static void RegisterDataTierModule(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}