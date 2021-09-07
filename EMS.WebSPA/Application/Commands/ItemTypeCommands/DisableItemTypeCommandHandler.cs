using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class DisableItemTypeCommandHandler : IRequestHandler<DisableItemTypeCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly IItemTypeRepository _itemTypeRepository;

        public DisableItemTypeCommandHandler(IEmsQuery emsQuery, IItemTypeRepository itemTypeRepository)
        {
            _emsQuery = emsQuery;
            _itemTypeRepository = itemTypeRepository;
        }
        public async Task<bool> Handle(DisableItemTypeCommand request, CancellationToken cancellationToken)
        {
            var itemType = await _itemTypeRepository.FindAsync(request.Id);
            if (itemType == null)
                throw new EMSDomainException("شناسه نامعتبر است");
            if (await _emsQuery.DoesItemTypeHasChild(request.Id))
                throw new EMSDomainException("غیرفعال سازی آیتم انتخابی غیر مجاز می باشد");
            itemType.Disable();
            return await _itemTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DisableItemTypeIdentifiedCommandHandler : IdentifierCommandHandler<DisableItemTypeCommand, bool>
    {
        public DisableItemTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, Identifier identifier) :
            base(mediator, requestManager, identifier)
        { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}