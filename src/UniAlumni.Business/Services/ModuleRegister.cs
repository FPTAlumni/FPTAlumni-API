using FirebaseAdmin.Auth;
using Microsoft.Extensions.DependencyInjection;
using UniAlumni.Business.Services.AlumniGroupService;
using UniAlumni.Business.Services.AlumniService;
using UniAlumni.Business.Services.AuthenticationService;
using UniAlumni.Business.Services.CategoryService;
using UniAlumni.Business.Services.ClassService;
using UniAlumni.Business.Services.CompanyService;
using UniAlumni.Business.Services.EventRegistrationService;
using UniAlumni.Business.Services.EventService;
using UniAlumni.Business.Services.GroupSrv;
using UniAlumni.Business.Services.MajorSrv;
using UniAlumni.Business.Services.NewsSrv;
using UniAlumni.Business.Services.UniversityService;

namespace UniAlumni.Business.Services
{
    public static class ModuleRegister
    {
        /// <summary>
        /// register all Services Module.
        /// </summary>
        /// <param name="services">service collection from startup.</param>
        public static void RegisterServicesModule(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAlumniSvc), typeof(AlumniSrv));
            services.AddScoped(typeof(IAuthenticationSvc), typeof(AuthenticationSvc));
            //services.AddScoped<FirebaseAuth, FirebaseAuth>();

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMajorService, MajorService>();
            
            services.AddScoped<ICategorySvc, CategorySvc>();
            services.AddScoped<ICompanySvc, CompanySvc>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUniversitySvc, UniversitySvc>();
            services.AddScoped<IRecruitmentService, RecruitmentService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IReferralService, ReferralService>();
            services.AddScoped<IClassSvc, ClassSvc>();
            services.AddScoped<IEventSvc, EventSvc>();
            services.AddScoped<IAlumniGroupSvc, AlumniGroupSvc>();
            services.AddScoped<IEventRegistrationSvc, EventRegistrationSvc>();
        }
    }
}