using MediatR;
namespace EMS.WebSPA.Application.Commands.AccountCommands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public ResetPasswordCommand(string userIdentity)
        {
            UserIdentity = userIdentity;
        }
        public string UserIdentity { get; private set; }

    }
}