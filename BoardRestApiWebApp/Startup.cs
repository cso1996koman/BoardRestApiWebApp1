using Autofac;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace RestApiProject
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.InitializeAutoMapper();
            services.AddDbContext(Configuration);
            services.AddRazorPages();
            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddMinimalMvc();
            services.AddElmahCore(Configuration, _siteSetting);
            services.AddJwtAuthentication(_siteSetting.JwtSettings);
            services.AddCustomApiVersioning();
            services.AddSwagger();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddServices();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.IntializeDatabase();
            app.UseCustomExceptionHandler();
            app.UseHosts(env);
            app.UseHttpsRedirection();
            app.UseElmahCore(_siteSetting);
            app.UseSwaggerAndUI();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(config =>
            {
                config.MapControllers(); 
            });
        }
    }
}
