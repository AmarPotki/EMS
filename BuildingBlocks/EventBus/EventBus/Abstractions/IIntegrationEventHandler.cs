using System.Threading.Tasks;
using Irvine.BuildingBlocks.EventBus.Events;

namespace Irvine.BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent eventMessage);
    }

    public interface IIntegrationEventHandler
    {
    }
}