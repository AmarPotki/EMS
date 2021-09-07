using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.LocationAggregate;
using Irvine.SeedWork.Domain;
using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class EditItemTypeCommandHandler : IRequestHandler<EditItemTypeCommand, bool>
    {
        private readonly ILocationRepository _locationRepository;
        public EditItemTypeCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<bool> Handle(EditItemTypeCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.FindAsync(request.Id);
            if (location == null) throw new DomainException("شناسه نامعتبر است");
            location.Edit(request.Name);
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class EditItemTypeIdentifiedCommandHandler : IdentifierCommandHandler<EditItemTypeCommand, bool>
    {
        public EditItemTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}