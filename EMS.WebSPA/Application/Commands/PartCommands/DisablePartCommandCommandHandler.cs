using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Domain.Exceptions;
using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class DisablePartCommandCommandHandler : IRequestHandler<DisablePartCommand, bool>{
        private readonly IPartRepository _partRepository;
        public DisablePartCommandCommandHandler( IPartRepository partRepository){
            _partRepository = partRepository;
        }
        public async Task<bool> Handle(DisablePartCommand request, CancellationToken cancellationToken){
            var part = await _partRepository.FindAsync(request.Id);
            if (part == null) throw new EMSDomainException("شناسه نامعتبر است");
            part.Archive();
            return await _partRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DisablePartIdentifiedCommandHandler : IdentifierCommandHandler<DisablePartCommand, bool>{
        public DisablePartIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}