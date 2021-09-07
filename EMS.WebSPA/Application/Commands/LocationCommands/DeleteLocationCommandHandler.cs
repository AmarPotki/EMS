using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.AggregatesModel.LocationAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;

namespace EMS.WebSPA.Application.Commands.LocationCommands
{
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly ILocationRepository _locationRepository;

        public DeleteLocationCommandHandler(IEmsQuery emsQuery, ILocationRepository locationRepository)
        {
            _emsQuery = emsQuery;
            _locationRepository = locationRepository;
        }
        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.FindAsync(request.Id);
            if (location == null)
                throw new EMSDomainException("شناسه نامعتبر است");
            var childern = await _emsQuery.GetLocationChildren(request.Id);
            if (childern.Any())
                throw new EMSDomainException("حذف این نود به علت داشتن فرزند غیرمجاز است");
            if (await _emsQuery.FaultsHasRecordWithSpecialLocationId(request.Id) || 
                await _emsQuery.FixUnitsHasRecordWithSpecialLocationId(request.Id))
                throw new EMSDomainException("شناسه این نود در موجودیت دیگری در حال استفاده است");
            _locationRepository.Remove(location);
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class DeleteLocationIdentifiedCommandHandler : IdentifierCommandHandler<DeleteLocationCommand, bool>
    {
        public DeleteLocationIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}