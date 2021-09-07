using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class MoveFaultCommand:IRequest<bool>{
        public MoveFaultCommand(int faultId, int fixUnitId, string description){
            FaultId = faultId;
            FixUnitId = fixUnitId;
            Description = description;
        }
        public int FaultId { get;private set; }
        public int FixUnitId { get;private set; }
        public string Description { get;private set; }

    }
}