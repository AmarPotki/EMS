using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Domain.Exceptions;
using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class CreatePartCommandCommandHandler : IRequestHandler<CreatePartCommand, bool>
    {
        private readonly IPartRepository _partRepository;
        public CreatePartCommandCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }
        public async Task<bool> Handle(CreatePartCommand request, CancellationToken cancellationToken)
        {
            if(await _partRepository.IsExistByNameAsync(request.Name)) throw new EMSDomainException("نام تکراری است");
            var part = new Part(request.Name);
            _partRepository.Add(part);
            return await _partRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

    }
    public class CreatePartIdentifiedCommandHandler : IdentifierCommandHandler<CreatePartCommand, bool>{
        public CreatePartIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
 
}