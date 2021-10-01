using FirebaseAdmin.Auth;
using Microsoft.Extensions.DependencyInjection;
using System;
using UniAlumni.Business.Services.AlumniService;
using UniAlumni.Business.Services.AuthenticationService;
using UniAlumni.Business.Services.GroupSrv;

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
        }
    }
}