using MediatR;
namespace EMS.WebSPA.Application.Commands.AccountCommands{
    public class LockOutCommand : IRequest<bool>
    {
        public LockOutCommand(string userIdentity){
            UserIdentity = userIdentity;
        }
        public string UserIdentity { get; private set; }

    }
}