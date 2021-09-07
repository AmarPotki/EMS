using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class FixFaultCommandHandler : IRequestHandler<FixFaultCommand, bool>
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IIdentityService _identityService;
        private readonly IFixUnitQuery _fixUnitQuery;
        public FixFaultCommandHandler(IFixUnitRepository fixUnitRepository, IIdentityService identityService,
            IFaultRepository faultRepository, IFixUnitQuery fixUnitQuery)
        {
            _fixUnitRepository = fixUnitRepository;
            _identityService = identityService;
            _faultRepository = faultRepository;
            _fixUnitQuery = fixUnitQuery;
        }
        public async Task<bool> Handle(FixFaultCommand request, CancellationToken cancellationToken)
        {
            var userGuid = _identityService.GetUserIdentity();
            var fault = await _faultRepository.GetFaultWithFaultResult(request.FaultId);
            if (fault == null) throw new EMSDomainException("شناسه نامعتبر است");
            if (!await _fixUnitQuery.IsValid(userGuid, fault.GetFixUnitId())) throw new EMSDomainException("شما مجاز به جابه جای این خرابی نیستید");
            if(!request.IsComplete)
            fault.AddFaultResult( request.Description, userGuid, fault.GetFixUnitId());
            else
                fault.FixFault(request.Description,userGuid,fault.GetFixUnitId());
            return await _faultRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class FixFaultIdentifiedCommandHandler : IdentifierCommandHandler<FixFaultCommand, bool>
    {
        public FixFaultIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}