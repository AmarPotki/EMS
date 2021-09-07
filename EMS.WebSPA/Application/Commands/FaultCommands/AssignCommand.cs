using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class AssignCommand:IRequest<bool>{
        public AssignCommand(int faultId, string userId){
            FaultId = faultId;
            UserId = userId;
        }
        public int FaultId { get;private set; }
        public string UserId { get;private set; }

    }
}