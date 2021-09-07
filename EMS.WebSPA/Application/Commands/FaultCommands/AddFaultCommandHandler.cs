using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace EMS.WebSPA.Application.Commands.FaultCommands
{
    public class AddFaultCommandHandler : IRequestHandler<AddFaultCommand, bool>
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IIdentityService _identityService;
        public AddFaultCommandHandler(IFixUnitRepository fixUnitRepository, IIdentityService identityService,
            IFaultRepository faultRepository)
        {
            _fixUnitRepository = fixUnitRepository;
            _identityService = identityService;
            _faultRepository = faultRepository;
        }
        public async Task<bool> Handle(AddFaultCommand request, CancellationToken cancellationToken)
        {
            var fixUnit = await _fixUnitRepository.GetFixUnit(request.LocationId, request.ItemTypeId);
            if (fixUnit == null) throw new EMSDomainException("واحدی برای دریافت خرابی شما تعریف نشده است");
            var user = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(user)) throw new EMSDomainException("شما مجاز به ثبت نمی باشید");
            var fault = new Fault(request.Title, request.Description, request.FaultTypeId, request.ItemTypeId,
                request.LocationId, user, fixUnit.Id);
            _faultRepository.Add(fault);
            return await _faultRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class AddFaultIdentifiedCommandHandler : IdentifierCommandHandler<AddFaultCommand, bool>
    {
        public AddFaultIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
 
}