using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.Business.Services.GroupService;
using UniAlumni.DataTier;
using UniAlumni.DataTier.Repositories.GroupRepo;

namespace UniAlumni.Business.Common
{
    public static class DependencyInjectionResolver
    {
        public static void InitializerDI(this IServiceCollection services)
        {
            services.AddScoped<DbContext, FPTAlumniContext>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();
        }
    }
}
