using Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Data;
using Core.Caching.Interfaces;
using Core.Caching;
using JobsClean.Web.Filters;
using FluentValidation.AspNetCore;
using Models.Validations.Account;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Core.Domains.Users;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Extensions.Hosting;

namespace JobsClean.Web.StartupConfiguration
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            services.AddDbContext<JSDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Data")));

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

            })
           .AddEntityFrameworkStores<JSDbContext>()
           .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "auth_cookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = redirectContext =>
                    {
                        redirectContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddControllers()          
            .AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                options.LocalizationEnabled = false;
                options.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>();
            });

            services.AddMemoryCache();
            services.AddSingleton<IStandingCache, InMemoryCache>();

            ////Add JSDbContext resolver 
            services.AddScoped<IDbContext>(provider => provider.GetService<JSDbContext>());
            services.AddScoped(typeof(IRepository<>), typeof(JSRepository<>));

            services.AddServices();

            services.AddAutoMapper(Assembly.GetCallingAssembly());

            if (!hostingEnvironment.IsProduction())
            {
                services.AddSwaggerGen();
            }


        }

    }
}
