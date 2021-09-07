using System;
using System.Threading;
using System.Threading.Tasks;
using EMS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace EMS.WebSPA.Application.Behaviors{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>{
        private readonly EmsContext _dbContext;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        public TransactionBehaviour(EmsContext dbContext,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger){
            _dbContext = dbContext ?? throw new ArgumentException(nameof(_dbContext));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next){
            var response = default(TResponse);
            try{
                var strategy = _dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () => {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");
                    await _dbContext.BeginTransactionAsync();
                    response = await next();
                    await _dbContext.CommitTransactionAsync();
                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
                });
                return response;
            }
            catch (Exception){
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}