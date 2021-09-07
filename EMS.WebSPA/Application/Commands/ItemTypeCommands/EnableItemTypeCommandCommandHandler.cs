using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.Exceptions;
using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class EnableItemTypeCommandCommandHandler : IRequestHandler<EnableItemTypeCommand, bool>{
        private readonly IItemTypeRepository _itemTypeRepository;
        public EnableItemTypeCommandCommandHandler(IItemTypeRepository itemTypeRepository){
            _itemTypeRepository = itemTypeRepository;
        }
        public async Task<bool> Handle(EnableItemTypeCommand request, CancellationToken cancellationToken){
            var itemType = await _itemTypeRepository.FindAsync(request.Id);
            if (itemType == null)
                throw new EMSDomainException("شناسه نامعتبر است");
            itemType.Enable();
            return await _itemTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class EnableItemTypeIdentifiedCommandHandler : IdentifierCommandHandler<EnableItemTypeCommand, bool>{
        public EnableItemTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) :
            base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
  
}