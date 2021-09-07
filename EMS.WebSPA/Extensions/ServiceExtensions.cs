using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace EMS.WebSPA.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static void ConfigureAntiforgery(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties.
                //options.FormFieldName = "AntiforgeryFieldname";
                //options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.HeaderName = "X-XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;

                options.Cookie.HttpOnly = false;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.Name = "EMSAntiforgeryCookie";
                options.Cookie.Domain = "localhost";
                options.Cookie.Path = "/";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = "EMSSessionCookie";
                options.Cookie.Domain = "localhost";
                options.Cookie.Path = "/";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }

        public static void ConfigureApi(this IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();
            ////services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            //services.AddApiVersioning(
            //    o =>
            //    {
            //        o.AssumeDefaultVersionWhenUnspecified = true;
            //        o.DefaultApiVersion = new ApiVersion(1, 0);
            //        //o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //    }
            //);
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
    }
}
