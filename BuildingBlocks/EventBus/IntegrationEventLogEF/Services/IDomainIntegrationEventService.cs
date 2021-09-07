using System.Threading.Tasks;
using Irvine.BuildingBlocks.EventBus.Events;

namespace BuildingBlocks.IntegrationEventLogEF.Services
{
    public interface IDomainIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}