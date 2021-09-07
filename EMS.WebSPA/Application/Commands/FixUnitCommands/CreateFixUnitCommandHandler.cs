using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.WebSPA.Application.Queries;
using Irvine.SeedWork.Domain;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class CreateFixUnitCommandHandler : IRequestHandler<CreateFixUnitCommand, bool>{
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IFixUnitQuery _fixUnitQuery;
        public CreateFixUnitCommandHandler(IFixUnitRepository fixUnitRepository, IFixUnitQuery fixUnitQuery){
            _fixUnitRepository = fixUnitRepository;
            _fixUnitQuery = fixUnitQuery;
        }
        public async Task<bool> Handle(CreateFixUnitCommand request, CancellationToken cancellationToken){
            if (await _fixUnitRepository.IsExistAsync(request.LocationId, request.ItemTypeId))
                throw new DomainException("برای این لوکیشن و نوع قبلا واحدی ثبت شده است");
            var fixUnit = new FixUnit(request.Description, request.Title, request.ItemTypeId, request.LocationId,
                request.Owner);
            var users = await _fixUnitQuery.GetMembers(request.Members);
            foreach (var mem in users) fixUnit.AddMember(mem.Id,mem.Name);
            _fixUnitRepository.Add(fixUnit);
            return await _fixUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class CreateFixUnitIdentifiedCommandHandler : IdentifierCommandHandler<CreateFixUnitCommand, bool>{
        public CreateFixUnitIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}