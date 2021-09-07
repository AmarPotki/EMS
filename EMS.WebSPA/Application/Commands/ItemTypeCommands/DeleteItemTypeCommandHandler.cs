using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;

namespace EMS.WebSPA.Application.Commands.ItemTypeCommands
{
    public class DeleteItemTypeCommandHandler : IRequestHandler<DeleteItemTypeCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly IItemTypeRepository _itemTypeRepository;

        public DeleteItemTypeCommandHandler(IEmsQuery emsQuery, IItemTypeRepository itemTypeRepository)
        {
            _emsQuery = emsQuery;
            _itemTypeRepository = itemTypeRepository;
        }
        public async Task<bool> Handle(DeleteItemTypeCommand request, CancellationToken cancellationToken)
        {
            var itemType = await _itemTypeRepository.FindAsync(request.Id);
            if (itemType == null)
                throw new EMSDomainException("شناسه نامعتبر است");
            var childern = await _emsQuery.GetItemTypeChildren(request.Id);
            if (childern.Any())
                throw new EMSDomainException("حذف این نود به علت داشتن فرزند غیرمجاز است");
            if (await _emsQuery.FaultsHasRecordWithSpecialItemTypeId(request.Id) ||
                 await _emsQuery.FixUnitsHasRecordWithSpecialItemTypeId(request.Id))
                throw new EMSDomainException("شناسه این نود در موجودیت دیگری در حال استفاده است");
            _itemTypeRepository.Remove(itemType);
            return await _itemTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }


    }

    public class DeleteItemTypeIdentifiedCommandHandler : IdentifierCommandHandler<DeleteItemTypeCommand, bool>
    {
        public DeleteItemTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}