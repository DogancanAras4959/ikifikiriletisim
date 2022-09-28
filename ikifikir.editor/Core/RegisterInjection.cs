using Flurl.Http.Configuration;
using ikifikir.CORE.Repository;
using ikifikir.CORE.UnitOfWork;
using ikifikir.DAL;
using ikifikir.ENGINES.Engines;
using ikifikir.ENGINES.Interface;
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

namespace ikifikir.editor.Core
{
    internal static class RegisterInjection
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
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(ITeamsService), typeof(TeamsService));
            services.AddTransient(typeof(ICategoryService), typeof(CategoryService));
            services.AddTransient(typeof(IProjectService), typeof(ProjectService));
            services.AddTransient(typeof(IGalleryService), typeof(GalleryService));
            services.AddTransient(typeof(IVideoService), typeof(VideoService));
            services.AddTransient(typeof(IPostService), typeof(PostService));
            services.AddTransient(typeof(IPricingService), typeof(PricingService));
            //services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}
