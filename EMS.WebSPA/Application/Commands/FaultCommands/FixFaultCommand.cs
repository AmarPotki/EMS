using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class FixFaultCommand : IRequest<bool>
    {
        public FixFaultCommand(int faultId, string description){
            FaultId = faultId;
            Description = description;
        }
        public int FaultId { get;private set; }
        public string Description { get; private set; }
        public bool IsComplete { get; set; }
    }
}