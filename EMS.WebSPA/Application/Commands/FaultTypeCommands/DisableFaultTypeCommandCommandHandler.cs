using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands
{
    public class DisableFaultTypeCommandCommandHandler : IRequestHandler<DisableFaultTypeCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly IFaultTypeRepository _faultTypeRepository;
        public DisableFaultTypeCommandCommandHandler(IEmsQuery emsQuery, IFaultTypeRepository faultTypeRepository)
        {
            _emsQuery = emsQuery;
            _faultTypeRepository = faultTypeRepository;
        }
        public async Task<bool> Handle(DisableFaultTypeCommand request, CancellationToken cancellationToken)
        {
            var faultType = await _faultTypeRepository.FindAsync(request.Id);
            if (faultType == null) throw new EMSDomainException("شناسه نامعتبر است");
            faultType.Archive();
            return await _faultTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DisableFaultTypeIdentifiedCommandHandler : IdentifierCommandHandler<DisableFaultTypeCommand, bool>
    {
        public DisableFaultTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}