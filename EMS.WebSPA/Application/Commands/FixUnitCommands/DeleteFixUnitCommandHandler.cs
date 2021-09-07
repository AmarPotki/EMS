using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class DeleteFixUnitCommandHandler : IRequestHandler<DeleteFixUnitCommand, bool>{
        private readonly IEmsQuery _emsQuery;
        private readonly IFixUnitRepository _fixUnitRepository;
        public DeleteFixUnitCommandHandler(IEmsQuery emsQuery, IFixUnitRepository fixUnitRepository){
            _emsQuery = emsQuery;
            _fixUnitRepository = fixUnitRepository;
        }
        public async Task<bool> Handle(DeleteFixUnitCommand request, CancellationToken cancellationToken){
            var fixUnit = await _fixUnitRepository.FindAsync(request.Id);
            if (fixUnit == null) throw new EMSDomainException("شناسه نامعتبر است");
            if (await _emsQuery.IsExistItems(request.Id))
                throw new EMSDomainException("شما قادر به حذف واحد خرابی که برای آن خرابی اعلام شده نیستید");
            _fixUnitRepository.Remove(fixUnit);
            return await _fixUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DeleteFixUnitIdentifiedCommandHandler : IdentifierCommandHandler<DeleteFixUnitCommand, bool>{
        public DeleteFixUnitIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return base.CreateResultForDuplicateRequest();
        }
    }
}