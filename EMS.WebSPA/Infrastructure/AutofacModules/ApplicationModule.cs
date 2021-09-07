using Autofac;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.AggregatesModel.LocationAggregate;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using EMS.Infrastructure.Repositories;
using EMS.WebSPA.Application.Queries;
using EMS.WebSPA.Services;
using IdentityServer4.Services;

namespace EMS.WebSPA.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }
        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthMessageSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
            builder.RegisterType<AuthMessageSender>()
                .As<ISmsSender>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EFLoginService>()
                .As<ILoginService<UserProfile>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RedirectService>()
                .As<IRedirectService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProfileService>()
                .As<IProfileService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<LocationRepository>()
                .As<ILocationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ItemTypeRepository>()
                .As<IItemTypeRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FixUnitRepository>()
                .As<IFixUnitRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserProfileRepository>()
                .As<IUserProfileRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FaultTypeRepository>()
                .As<IFaultTypeRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FaultRepository>()
                .As<IFaultRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PartRepository>()
                .As<IPartRepository>()
                .InstancePerLifetimeScope();
            //builder.RegisterType<FaultTypeRepository>()
            //    .As<IFaultRepository>()
            //    .InstancePerLifetimeScope();
            builder.RegisterType<EmsQuery>()
                .As<IEmsQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FixUnitQuery>()
                .As<IFixUnitQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FaultQuery>()
                .As<IFaultQuery>()
                .InstancePerLifetimeScope();
        }
    }
}