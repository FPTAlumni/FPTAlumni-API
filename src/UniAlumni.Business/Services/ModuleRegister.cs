using FirebaseAdmin.Auth;
using Microsoft.Extensions.DependencyInjection;
using UniAlumni.Business.Services.Interface;

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
            
            services.AddScoped<FirebaseAuth, FirebaseAuth>();
        }
    }
}