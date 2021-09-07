using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class AddPartToFaultCommand : IRequest<bool>{
        public AddPartToFaultCommand(int faultId, int partId, int total){
            FaultId = faultId;
            PartId = partId;
            Total = total;
        }
        public int FaultId { get;private set; }
        public int PartId { get; private set; }
        public int Total { get; private set; }
    }
}