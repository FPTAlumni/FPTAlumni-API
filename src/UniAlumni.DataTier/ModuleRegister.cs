using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.CategoryRepo;
using UniAlumni.DataTier.Repositories.CompanyRepo;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Repositories.MajorRepo;

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
        public static IServiceCollection RegisterDataTierModule(this IServiceCollection services)
        {
            services.AddScoped<DbContext, FPTAlumniContext>();
            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            
            services.AddScoped<IAlumniRepository, AlumniRepository>();

            services.AddScoped<IGroupRepository, GroupRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IMajorRepository, MajorRepository>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}