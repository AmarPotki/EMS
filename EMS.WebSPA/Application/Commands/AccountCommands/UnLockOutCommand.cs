using MediatR;
namespace EMS.WebSPA.Application.Commands.AccountCommands
{
    public class UnLockOutCommand : IRequest<bool>
    {
        public UnLockOutCommand(string userIdentity)
        {
            UserIdentity = userIdentity;
        }
        public string UserIdentity { get; private set; }

    }
}