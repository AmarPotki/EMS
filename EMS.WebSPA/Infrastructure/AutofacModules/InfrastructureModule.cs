using Autofac;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Identity;
using BuildingBlocks.Infrastructure.Services.NotificationHub;
using BuildingBlocks.IntegrationEventLogEF.Services;
using EMS.Infrastructure;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace EMS.WebSPA.Infrastructure.AutofacModules
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .InstancePerDependency();

            builder.RegisterType<IdentityService>()
                .As<IIdentityService>()
                .InstancePerDependency();

            builder.RegisterType<NotificationService>()
              .As<INotificationService>()
              .InstancePerDependency();

            builder.RegisterType<RequestManager<EmsContext>>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PersistedGrantDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Identifier>()
                .As<Identifier>()
                .InstancePerLifetimeScope();

            #region Integration Event Section

            builder.RegisterType<EmsContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<DomainIntegrationEventService<CandidateContext>>()
            //    .As<IDomainIntegrationEventService>()
            //    .InstancePerDependency();

            #endregion
        }
    }
}