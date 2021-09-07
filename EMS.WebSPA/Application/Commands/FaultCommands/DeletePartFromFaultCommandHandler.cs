using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class DeletePartFromFaultCommandHandler : IRequestHandler<DeletePartFromFaultCommand, bool>
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IIdentityService _identityService;
        private readonly IFixUnitQuery _fixUnitQuery;
        public DeletePartFromFaultCommandHandler(IIdentityService identityService,
            IFaultRepository faultRepository, IFixUnitQuery fixUnitQuery)
        {
            _identityService = identityService;
            _faultRepository = faultRepository;
            _fixUnitQuery = fixUnitQuery;
        }
        public async Task<bool> Handle(DeletePartFromFaultCommand request, CancellationToken cancellationToken)
        {
            var userGuid = _identityService.GetUserIdentity();
            var fault = await _faultRepository.GetFaultWithPart(request.FaultId);
            if (fault == null) throw new EMSDomainException("شناسه نامعتبر است");
            if (!await _fixUnitQuery.IsValid(userGuid, fault.GetFixUnitId()))
                throw new EMSDomainException("شما مجاز به حذف این قطعه نیستید");

            fault.DeletePart(request.PartId);
            return await _faultRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DeletePartFromFaultIdentifiedCommandHandler : IdentifierCommandHandler<DeletePartFromFaultCommand, bool>
    {
        public DeletePartFromFaultIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}