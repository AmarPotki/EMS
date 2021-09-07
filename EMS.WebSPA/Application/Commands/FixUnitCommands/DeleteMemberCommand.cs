using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class DeleteMemberCommand : IRequest<bool>
    {
        public DeleteMemberCommand(int fixUnitId, string userGuid)
        {
            FixUnitId = fixUnitId;
            UserGuid = userGuid;
        }
        public string UserGuid { get; private set; }
        public int FixUnitId { get; private set; }
    }
}