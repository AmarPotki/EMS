using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.LocationAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;

namespace EMS.WebSPA.Application.Commands.LocationCommands
{
    public class DisableLocationCommandHandler : IRequestHandler<DisableLocationCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly ILocationRepository _locationRepository;

        public DisableLocationCommandHandler(IEmsQuery emsQuery, ILocationRepository locationRepository)
        {
            _emsQuery = emsQuery;
            _locationRepository = locationRepository;
        }
        public async Task<bool> Handle(DisableLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.FindAsync(request.Id);
            if (location == null)
                throw new EMSDomainException("شناسه نامعتبر است");
            if (await _emsQuery.DoesLocationHasChild(request.Id))
                throw new EMSDomainException("غیرفعال سازی آیتم انتخابی غیر مجاز می باشد");
            location.Disable();
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class DisableLocationIdentifiedCommandHandler : IdentifierCommandHandler<DisableLocationCommand, bool>
    {
        public DisableLocationIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, Identifier identifier) :
            base(mediator, requestManager, identifier)
        { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}