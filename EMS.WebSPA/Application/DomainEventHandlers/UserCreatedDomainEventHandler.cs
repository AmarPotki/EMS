using System.Threading;
using System.Threading.Tasks;
using EMS.Domain.Events;
using MediatR;
namespace EMS.WebSPA.Application.DomainEventHandlers{
    public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken){
            throw new System.NotImplementedException();
        }
    }
}