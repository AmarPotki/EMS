using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class EditPartCommandCommandHandler : IRequestHandler<EditPartCommand, bool>{
        private readonly IEmsQuery _emsQuery;
        private readonly IPartRepository _partRepository;
        public EditPartCommandCommandHandler(IEmsQuery emsQuery, IPartRepository partRepository){
            _emsQuery = emsQuery;
            _partRepository = partRepository;
        }
        public async Task<bool> Handle(EditPartCommand request, CancellationToken cancellationToken){
            if (await _emsQuery.IsExistItemWithPart(request.Id))
                throw new EMSDomainException("با این قطعه، آیتم ثبت شده است ، شما قادر به تغییر نام نیستید");
            var part =await _partRepository.FindAsync(request.Id);
            if (part == null) throw new EMSDomainException("شناسه نامعتبر است");
            part.Update(request.Name);
            return await _partRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class EditPartIdentifiedCommandHandler : IdentifierCommandHandler<EditPartCommand, bool>{
        public EditPartIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}