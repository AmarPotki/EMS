using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.WebSPA.Application.Queries;
using Irvine.SeedWork.Domain;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>{
        private readonly IFaultQuery _faultQuery;
        private readonly IFixUnitRepository _fixUnitRepository;
        public DeleteMemberCommandHandler(IFixUnitRepository fixUnitRepository, IFaultQuery faultQuery){
            _fixUnitRepository = fixUnitRepository;
            _faultQuery = faultQuery;
        }
        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken){
            var fixUnit = await _fixUnitRepository.GetFixUnitWithMembers(request.FixUnitId);
            if (fixUnit is null)
                throw new DomainException("شناسه نا معتبر است");
            //if (await _faultQuery.IsExistFaultResult(request.UserGuid))
            //    throw new DomainException(
            //        "شما نمی توانید کاربری که اقدامی در این واحد انجام داده را حذف کنید ، اگر نیاز به گرفتن دسترسی از این یوزر را دارید آن را غیر فعال کنید");
            fixUnit.RemoveMember(request.UserGuid);
            return await _fixUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DeleteMemberIdentifiedCommandHandler : IdentifierCommandHandler<DeleteMemberCommand, bool>{
        public DeleteMemberIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}