using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.WebSPA.Application.Queries;
using Irvine.SeedWork.Domain;
using MediatR;

namespace EMS.WebSPA.Application.Commands.FixUnitCommands
{
    public class EditFixUnitCommandHandler : IRequestHandler<EditFixUnitCommand, bool>
    {
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IEmsQuery _emsQuery;

        public EditFixUnitCommandHandler(IFixUnitRepository fixUnitRepository, IEmsQuery emsQuery)
        {
            _emsQuery = emsQuery;
            _fixUnitRepository = fixUnitRepository;
        }
        public async Task<bool> Handle(EditFixUnitCommand request, CancellationToken cancellationToken)
        {
            var fixUnit = await _fixUnitRepository.FindAsync(request.Id);
            if (fixUnit == null) throw new DomainException("شناسه نامعتبر است");
            if(await _emsQuery.IsExistItems(fixUnit.Id)) throw new DomainException(" این یک یا چند خرابی دارد شما مجاز به حذف نمی باشید ");
            fixUnit.Edit(request.Description, request.Title, request.ItemTypeId, request.LocationId, request.Owner);
            return await _fixUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class EditFixUnitIdentifiedCommandHandler : IdentifierCommandHandler<EditFixUnitCommand, bool>
    {
        public EditFixUnitIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, Identifier identifier) :
            base(mediator, requestManager, identifier)
        {
        }
        protected override bool CreateResultForDuplicateRequest()
        {
            return base.CreateResultForDuplicateRequest();
        }
    }
}


