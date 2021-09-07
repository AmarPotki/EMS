using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Domain.Exceptions;
using EMS.Infrastructure.Repositories;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class DeletePartCommandCommandHandler : IRequestHandler<DeletePartCommand, bool>
    {
        private readonly IEmsQuery _emsQuery;
        private readonly IPartRepository _partRepository;
        public DeletePartCommandCommandHandler(IEmsQuery emsQuery, IPartRepository partRepository)
        {
            _emsQuery = emsQuery;
            _partRepository = partRepository;
        }
        public async Task<bool> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            if (await _emsQuery.IsExistItemWithPart(request.Id))
                throw new EMSDomainException("با این قطعه، آیتم ثبت شده است ، شما قادر به حذف نیستید");
            var part = await _partRepository.FindAsync(request.Id);
            if(part==null) throw new EMSDomainException("شناسه نامعتبر است");
            _partRepository.Remove(part);
            return await _partRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class DeletePartIdentifiedCommandHandler : IdentifierCommandHandler<DeletePartCommand, bool>
    {
        public DeletePartIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
   
}