using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.Exceptions;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands{
    public class CreateFaultTypeCommandCommandHandler : IRequestHandler<CreateFaultTypeCommand, bool>{
        private readonly IFaultTypeRepository _faultTypeRepository;
        public CreateFaultTypeCommandCommandHandler(IFaultTypeRepository faultTypeRepository){
            _faultTypeRepository = faultTypeRepository;
        }
        public async Task<bool> Handle(CreateFaultTypeCommand request, CancellationToken cancellationToken){
            if (await _faultTypeRepository.IsExistByNameAsync(request.Name))
                throw new EMSDomainException("نام تکراری است");
            var faultType = new FaultType(request.Name);
            _faultTypeRepository.Add(faultType);
            return await _faultTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class CreateFaultTypeIdentifiedCommandHandler : IdentifierCommandHandler<CreateFaultTypeCommand, bool>{
        public CreateFaultTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}