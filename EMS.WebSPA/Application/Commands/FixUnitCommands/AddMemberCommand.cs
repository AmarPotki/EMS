using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class AddMemberCommand : IRequest<bool>{
        public AddMemberCommand(int fixUnitId, string userGuid){
            FixUnitId = fixUnitId;
            UserGuid = userGuid;
        }
        public string UserGuid { get;private set; }
        public int FixUnitId { get;private set; }
    }
}