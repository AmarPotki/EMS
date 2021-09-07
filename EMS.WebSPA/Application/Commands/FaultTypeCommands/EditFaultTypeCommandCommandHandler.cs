using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands
{
    public class EditFaultTypeCommandCommandHandler : IRequestHandler<EditFaultTypeCommand, bool>{
        private readonly IEmsQuery _emsQuery;
        private readonly IFaultTypeRepository _faultTypeRepository;
        public EditFaultTypeCommandCommandHandler(IEmsQuery emsQuery, IFaultTypeRepository faultTypeRepository){
            _emsQuery = emsQuery;
            _faultTypeRepository = faultTypeRepository;
        }
        public async Task<bool> Handle(EditFaultTypeCommand request, CancellationToken cancellationToken){
            if (await _emsQuery.IsExistItemWithFaultType(request.Id))
                throw new EMSDomainException("با این نوع، آیتم ثبت شده است ، شما قادر به تغییر نام نیستید");
            var faultType =await _faultTypeRepository.FindAsync(request.Id);
            if (faultType == null) throw new EMSDomainException("شناسه نامعتبر است");
            faultType.Update(request.Name);
            return await _faultTypeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class EditFaultTypeIdentifiedCommandHandler : IdentifierCommandHandler<EditFaultTypeCommand, bool>{
        public EditFaultTypeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true; // Ignore duplicate requests for processing order.
        }
    }
}