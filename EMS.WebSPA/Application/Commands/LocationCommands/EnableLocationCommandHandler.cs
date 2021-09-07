using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.LocationAggregate;
using EMS.Domain.Exceptions;
using MediatR;
namespace EMS.WebSPA.Application.Commands.LocationCommands{
    public class EnableLocationCommandHandler : IRequestHandler<EnableLocationCommand, bool>{
        private readonly ILocationRepository _locationRepository;
        public EnableLocationCommandHandler(ILocationRepository locationRepository){
            _locationRepository = locationRepository;
        }
        public async Task<bool> Handle(EnableLocationCommand request, CancellationToken cancellationToken){
            var location = await _locationRepository.FindAsync(request.Id);
            if (location == null)
                throw new EMSDomainException("شناسه نامعتبر است");
            location.Enable();
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class EnableLocationIdentifiedCommandHandler : IdentifierCommandHandler<EnableLocationCommand, bool>{
        public EnableLocationIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) :
            base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}