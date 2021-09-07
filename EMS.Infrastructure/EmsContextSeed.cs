using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.FaultAggregate;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Polly;
namespace EMS.Infrastructure
{
  public  class EmsContextSeed
    {
        public async Task SeedAsync(EmsContext context, IHostingEnvironment env,
            ILogger<EmsContextSeed> logger){
            var policy = CreatePolicy(logger, nameof(context));

            await policy.ExecuteAsync(async () => {
                using (context)
                {
                    if (!await context.FaultStatuses.AnyAsync())
                    {
                        context.FaultStatuses.AddRange(GetPredefienedFaultStatus());
                    }

                    await context.SaveChangesAsync();
                }
            });
        }
        private IEnumerable<FaultStatus> GetPredefienedFaultStatus()
        {
            yield return FaultStatus.Submitted;
            yield return FaultStatus.Assigned;
            yield return FaultStatus.Doing;
            yield return FaultStatus.Moved;
            yield return FaultStatus.Done;
        }
        private Policy CreatePolicy(ILogger<EmsContextSeed> logger, string prefix, int retries = 7)
        {
            return Policy.Handle<SqlException>().WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timespan, retry, ctx) =>
                {
                    logger.LogTrace(
                        $"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                }
            );
        }
    }
}
