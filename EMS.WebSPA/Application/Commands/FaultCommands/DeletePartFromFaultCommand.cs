using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public  class DeletePartFromFaultCommand : IRequest<bool>
    {
        public DeletePartFromFaultCommand(int faultId, int partId){
            FaultId = faultId;
            PartId = partId;
        }
        public int FaultId { get; private set; }
        public int PartId { get; private set; }
    }
}