using System;
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
using Unit = EMS.Domain.AggregatesModel.FaultAggregate.Unit;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class MoveFaultCommandHandler : IRequestHandler<MoveFaultCommand, bool>
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IIdentityService _identityService;
        private readonly IFixUnitQuery _fixUnitQuery;
        public MoveFaultCommandHandler(IFixUnitRepository fixUnitRepository, IIdentityService identityService,
            IFaultRepository faultRepository, IFixUnitQuery fixUnitQuery)
        {
            _fixUnitRepository = fixUnitRepository;
            _identityService = identityService;
            _faultRepository = faultRepository;
            _fixUnitQuery = fixUnitQuery;
        }
        public async Task<bool> Handle(MoveFaultCommand request, CancellationToken cancellationToken)
        {
            var userGuid = _identityService.GetUserIdentity();
            var fault = await _faultRepository.GetFaultWithFlowAndFaultResult(request.FaultId);
            if (fault == null) throw new EMSDomainException("شناسه نامعتبر است");
            if (!await _fixUnitQuery.IsValid(userGuid, fault.GetFixUnitId())) throw new EMSDomainException("شما مجاز به جابه جای این خرابی نیستید");
            var fixUnit = await _fixUnitRepository.FindAsync(fault.GetFixUnitId());
            var fixUnitTo = await _fixUnitRepository.FindAsync(request.FixUnitId);
            fault.MoveToAnotherFixUnit(new Unit(fixUnit.Title,fixUnit.Id), new Unit(fixUnitTo.Title, fixUnitTo.Id),DateTimeOffset.Now,request.Description,userGuid);
            return await _faultRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class MoveFaultIdentifiedCommandHandler : IdentifierCommandHandler<MoveFaultCommand, bool>
    {
        public MoveFaultIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
 
}