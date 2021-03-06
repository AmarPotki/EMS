using System.Reflection;
using Autofac;
using EMS.WebSPA.Application.Behaviors;
using EMS.WebSPA.Application.Commands.LocationCommands;
using EMS.WebSPA.Application.DomainEventHandlers;
using EMS.WebSPA.Application.Validations;
using FluentValidation;
using MediatR;
using Module = Autofac.Module;
namespace EMS.WebSPA.Infrastructure.AutofacModules{
    public class MediatorModule : Module{
        protected override void Load(ContainerBuilder builder){
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            ////             Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateLocationCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(UserCreatedDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(CreateLocationCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
            builder.Register<ServiceFactory>(context => {
                var componentContext = context.Resolve<IComponentContext>();
                return t => {
                    object o;
                    return componentContext.TryResolve(t, out o) ? o : null;
                };
            });
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}