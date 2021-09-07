using System;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.Services.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid id);

        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}