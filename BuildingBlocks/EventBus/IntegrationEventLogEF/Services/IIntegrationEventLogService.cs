using System.Data.Common;
using System.Threading.Tasks;
using Irvine.BuildingBlocks.EventBus.Events;

namespace BuildingBlocks.IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IntegrationEvent @event, DbTransaction transaction);
        Task MarkEventAsPublishedAsync(IntegrationEvent @event);
    }
}