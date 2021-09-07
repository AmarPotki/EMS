using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace BuildingBlocks.Infrastructure.Services.TelemetryProcessor
{
    public class HealthCheckFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor _next { get; set; }

        public HealthCheckFilter(ITelemetryProcessor next)
        {
            this._next = next;
        }

        public void Process(ITelemetry item)
        {
            if (item?.Context?.Properties != null)
            {
                if (item.Context.Properties.ContainsKey("Path") && (item.Context.Properties["Path"].Contains("/hc") || item.Context.Properties["Path"].Contains("/liveness")))
                {
                    return;
                }
            }
            _next.Process(item);
        }
    }
}