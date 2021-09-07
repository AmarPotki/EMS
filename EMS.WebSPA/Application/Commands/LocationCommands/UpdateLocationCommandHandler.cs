using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.LocationAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace EMS.WebSPA.Application.Commands.LocationCommands
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationCommandHandler(IEmsQuery emsQuery,ILocationRepository locationRepository)
        {
            _emsQuery = emsQuery;
            _locationRepository = locationRepository;
        }
        public async Task<bool> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.FindAsync(request.Id);
            if (location == null)
                throw new EMSDomainException("شناسه نامعتبر است");
             if (await _emsQuery.DoesLocationHasChild(request.Id))
                throw new EMSDomainException("تغییر این نود به علت داشتن فرزند غیرقانونی است");
            if (await _emsQuery.FaultsHasRecordWithSpecialLocationId(request.Id) ||
                await _emsQuery.FixUnitsHasRecordWithSpecialLocationId(request.Id))
                throw new EMSDomainException("شناسه این نود در موجودیت دیگری در حال استفاده است");
            location.Edit(request.Name);
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class UpdateLocationCommandIdentifiedCommandHandler : IdentifierCommandHandler<UpdateLocationCommand, bool>
    {
        public UpdateLocationCommandIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, Identifier identifier) :
            base(mediator, requestManager, identifier)
        { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}

