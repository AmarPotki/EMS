using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using MediatR;

namespace BuildingBlocks.Infrastructure.Services.Commands
{
    public class IdentifierCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
        where T : IRequest<R>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        private readonly Identifier _identifier;

        public IdentifierCommandHandler(IMediator mediator, IRequestManager requestManager, Identifier identifier)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
            _requestManager = requestManager ?? throw new ArgumentNullException(nameof(requestManager));
            _identifier = identifier;
        }

        protected virtual R CreateResultForDuplicateRequest()
        {
            return default(R);
        }

        public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
        {
            _identifier.RequestId = message.Id;
            var alreadyExists = await _requestManager.ExistAsync(message.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await _requestManager.CreateRequestForCommandAsync<T>(message.Id);

                var result = await _mediator.Send(message.Command);

                return result;
            }
        }
    }
}