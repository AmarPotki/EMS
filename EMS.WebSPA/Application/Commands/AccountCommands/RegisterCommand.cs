using MediatR;
namespace EMS.WebSPA.Application.Commands.AccountCommands
{
    public class RegisterCommand : IRequest<bool>{
        public RegisterCommand(string fullName, string role, string userName, string password){
            FullName = fullName;
            Role = role;
            UserName = userName;
            Password = password;
        }
        public string FullName { get;private set; }
        public string Role { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}