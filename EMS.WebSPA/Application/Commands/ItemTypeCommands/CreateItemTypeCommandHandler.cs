using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using Irvine.SeedWork.Domain;
using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class CreateItemTypeCommandHandler : IRequestHandler<CreateItemTypeCommand,bool>{
        private readonly IItemTypeRepository _itemTypeRepository;
        public CreateItemTypeCommandHandler(IItemTypeRepository itemTypeRepository){
            _itemTypeRepository = itemTypeRepository;
        }
        public async Task<bool> Handle(CreateItemTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId != null)
            {
                if (!await _itemTypeRepository.IsValid(request.ParentId.Value))
                {
                    throw new DomainException("شناسه پدر نامعتبر است");
                }

            }
            var location = new ItemType(request.Name, request.ParentId);
            _itemTypeRepository.Add(location);
            return await _itemTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

    }
    // Use for Idempotency in Command process
    public class CreateItemTypeIdentifiedCommandHandler : IdentifierCommandHandler<CreateItemTypeCommand, bool>
    {
        public CreateItemTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, Identifier identifier) : base(mediator, requestManager, identifier)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
  
}