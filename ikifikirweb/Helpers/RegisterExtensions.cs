using ikifikir.CORE.UnitOfWork;
using ikifikir.DAL;
using ikifikir.ENGINES.Engines;
using ikifikir.ENGINES.Interface;
using ikifikirweb.Models.reCaptchaConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Helpers
{
    internal static class RegisterExtensions
    {
        internal static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var contextConnectionString = configuration.GetConnectionString("Default");
            services.AddDbContextPool<ikifikirdbcontext>(x => x.UseSqlServer(contextConnectionString, o =>
            {
                o.EnableRetryOnFailure(3);
            })
                .EnableSensitiveDataLogging(environment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        internal static void AddInjections(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(ICategoryService), typeof(CategoryService));
            services.AddTransient(typeof(IProjectService), typeof(ProjectService));
            services.AddTransient(typeof(IGalleryService), typeof(GalleryService));
            services.AddTransient(typeof(ITeamsService), typeof(TeamsService));
            services.AddTransient(typeof(IEmailSender), typeof(EmailSender));
            services.AddTransient(typeof(IVideoService), typeof(VideoService));
            services.AddTransient(typeof(IPostService), typeof(PostService));
            services.AddTransient(typeof(IPricingService), typeof(PricingService));
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddTransient<reCaptchaService>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}
