using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.LocationAggregate;
using Irvine.SeedWork.Domain;
namespace EMS.WebSPA.Application.Commands.LocationCommands
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, bool>{
        private readonly ILocationRepository _locationRepository;
        public CreateLocationCommandHandler(ILocationRepository locationRepository){
            _locationRepository = locationRepository;
        }
        public async Task<bool> Handle(CreateLocationCommand request, CancellationToken cancellationToken){
            if (request.ParentId != null){
                if (!await _locationRepository.IsValid(request.ParentId.Value)){
                    throw new DomainException("شناسه پدر نامعتبر است");
                }

            }
            var location = new Location(request.Name,request.ParentId);
            _locationRepository.Add(location);
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    // Use for Idempotency in Command process
    public class CreateLocationIdentifiedCommandHandler : IdentifierCommandHandler<CreateLocationCommand, bool>
    {
        public CreateLocationIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}