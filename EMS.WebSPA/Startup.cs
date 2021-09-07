using Autofac;
using Autofac.Extensions.DependencyInjection;
using BuildingBlocks.Infrastructure.Services.Filters;
using BuildingBlocks.Infrastructure.Services.Startup;
using BuildingBlocks.IntegrationEventLogEF;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using EMS.Infrastructure;
using EMS.WebSPA.Application.Commands;
using EMS.WebSPA.Configuration;
using EMS.WebSPA.Infrastructure.AutofacModules;
using EMS.WebSPA.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using EMS.WebSPA.Data.Tenant;
using EMS.WebSPA.Extensions;
namespace EMS.WebSPA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();
           // services.AddMediatR(typeof(CreateLocationCommandHandler));


            services.Configure<AppSettings>(Configuration);
            StartupExtension.RegisterDomainDbContext<EmsContext>(services, Configuration);
            StartupExtension.RegisterIntegrationEventDbContext<IntegrationEventLogContext>(services, Configuration);
            services.AddSwaggerDocument();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
             string connectionString = Configuration["IdentityConnectionString"];
               // @"Data Source=srvsql2014;Initial Catalog=EMSIdentityDB;Persist Security Info=True;User ID=sa;Password=SP_F@rm;";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            })
               .AddJwtBearer(jwt =>
               {
                   jwt.Authority = "https://localhost:44392";
                   jwt.RequireHttpsMetadata = false;
                   jwt.Audience = "emsapi";
               });
            services.AddCors();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentity<UserProfile, IdentityRole>()
                .AddEntityFrameworkStores<UserProfileContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<UserProfile>()
                .AddDeveloperSigningCredential()
                //.AddSigningCredential(new X509Certificate2(@".\IdentityServer4Cert\RahyabAPI.pfx", "aA123456"))
                //  .AddTestUsers(ServiceConfiguration.GetUsers())
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;

                }).Services.AddTransient<IProfileService, ProfileService>();



            services.AddEntityFrameworkSqlServer()
                .AddDbContext<UserProfileContext>(options =>
                {
                    options.UseSqlServer(connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        });

                }
                //  ,ServiceLifetime
                //    .Scoped //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request));
                );

            //multi tenant
            services.AddMultiTenant()
                .WithEFCoreStore<TenantDbContext, AppTenantInfo>().
                WithHostStrategy();

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

             container.RegisterModule(new MediatorModule());
            container.RegisterModule(new InfrastructureModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            InitializeDatabase(app);
            app.UseIdentityServer();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSwagger(c => { c.Path = "/swagger/v1/swagger.json"; });
            app.UseSwaggerUi3();
            app.UseCors(builder =>
                builder.AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
            app.UseMultiTenant();
            app.UseMiddleware<TenantMiddleWare>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<TenantDbContext>().Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in ServiceConfiguration.GetClients(Configuration["SiteUrl"]))
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in ServiceConfiguration.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in ServiceConfiguration.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
