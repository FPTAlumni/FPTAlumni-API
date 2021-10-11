using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UniAlumni.Business;
using UniAlumni.DataTier;
using UniAlumni.DataTier.AutoMapperModule;
using UniAlumni.WebAPI.Configurations;

namespace UniAlumni.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Firebase Services
            AddFireBaseAsync();
            // Allow CORS
            services.AddCors();
            // register (Authentication - JWT) Module 
            services.RegisterSecurityModule(Configuration);

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                o.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                
                o.SerializerSettings.Converters.Add(new StringEnumConverter()
                {
                    AllowIntegerValues = true
                });
                
            });

            // register (DI) Core Modules
            services.RegisterDataTierModule();
            services.RegisterBusinessModule();
            // register (Swagger) Module
            services.RegisterSwaggerModule();

            services.ConfigureAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseDeveloperExceptionPage();

            app.UseApplicationSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseApplicationSecurity();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public virtual FirebaseApp AddFireBaseAsync()
        {
            // Get Current Path
            var currentDirectory = Directory.GetCurrentDirectory();
            // Path of firebase.json
            var jsonFirebasePath = Path.Combine(currentDirectory, "Cert", "firebase.json");
            // Initialize the default app
            var defaultApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(jsonFirebasePath)
            });
            return defaultApp;
        }
    }
}