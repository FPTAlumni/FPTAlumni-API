using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories;
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.CategoryRepo;
using UniAlumni.DataTier.Repositories.CompanyRepo;
using UniAlumni.DataTier.Repositories.GroupRepo;
using UniAlumni.DataTier.Repositories.MajorRepo;
using UniAlumni.DataTier.Repositories.NewsRepo;
using UniAlumni.DataTier.Repositories.RecruitmentRepo;
using UniAlumni.DataTier.Repositories.ReferralRepo;
using UniAlumni.DataTier.Repositories.TagNewsRepo;
using UniAlumni.DataTier.Repositories.TagRepo;
using UniAlumni.DataTier.Repositories.UniversityRepo;
using UniAlumni.DataTier.Repositories.VoucherRepo;

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

            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<ITagNewsRepository, TagNewsRepository>();
            
            services.AddScoped<IUniversityRepository, UniversityRepository>();

            services.AddScoped<IAlumniGroupRepository, AlumniGroupRepository>();

            services.AddScoped<IRecruitmentRepository, RecruitmentRepository>();

            services.AddScoped<IVoucherRepository, VoucherRepository>();

            services.AddScoped<IReferralRepository, ReferralRepository>();

            return services;
        }
    }
}