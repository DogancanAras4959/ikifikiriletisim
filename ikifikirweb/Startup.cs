using AutoMapper;
using ikifikir.CORE.EmailConfig;
using ikifikirweb.Helpers;
using ikifikirweb.Models.reCaptchaConfig;
using ikifikirweb.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options => { options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); });
            services.AddDbContextDI(_configuration, Environment);
            services.AddInjections();
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddDistributedMemoryCache();

            services.Configure<reCaptchaSettings>(_configuration.GetSection("Google"));
            services.Configure<EmailConfiguration>(_configuration.GetSection("EmailConfiguration"));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectProfiles());
                mc.AddProfile(new TeamProfiles());
                mc.AddProfile(new CategoryProfiles());
                mc.AddProfile(new PostProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/anasayfa/hata/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "calisma",
                pattern: "/calisma/{Id}/{projectName}", new { controller = "anasayfa", action = "calisma" });

                endpoints.MapControllerRoute(
                name: "detay",
                pattern: "/detay/{Id}/{title}", new { controller = "blog", action = "detay" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=anasayfa}/{action=sayfa}/{id?}");
            });
        }
    }
}
