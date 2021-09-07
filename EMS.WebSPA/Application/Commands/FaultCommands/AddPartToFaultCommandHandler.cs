using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class AddPartToFaultCommandHandler : IRequestHandler<AddPartToFaultCommand, bool>
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IIdentityService _identityService;
        private readonly IFixUnitQuery _fixUnitQuery;
        private readonly IPartRepository _partRepository;
        public AddPartToFaultCommandHandler(  IIdentityService identityService,
            IFaultRepository faultRepository, IFixUnitQuery fixUnitQuery, IPartRepository partRepository)
        {
            _identityService = identityService;
            _faultRepository = faultRepository;
            _fixUnitQuery = fixUnitQuery;
            _partRepository = partRepository;
        }
        public async Task<bool> Handle(AddPartToFaultCommand request, CancellationToken cancellationToken)
        {
            var userGuid = _identityService.GetUserIdentity();
            var fault = await _faultRepository.GetFaultWithPart(request.FaultId);
            if (fault == null) throw new EMSDomainException("شناسه نامعتبر است");
            if (!await _fixUnitQuery.IsValid(userGuid, fault.GetFixUnitId())) throw new EMSDomainException("شما مجاز اضافه کردن قطعه نیستید");
            var part = await _partRepository.FindAsync(request.PartId);
            if (part == null) throw new EMSDomainException("شناسه قطعه نامعتبر است");
            fault.AddConsumePart(new ConsumePart(part.Name,part.Id,request.Total));
            return await _faultRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class AddPartToFaultIdentifiedCommandHandler : IdentifierCommandHandler<AddPartToFaultCommand, bool>
    {
        public AddPartToFaultIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
   
}