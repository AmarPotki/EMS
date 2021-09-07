using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class CreatePartCommand : IRequest<bool>
    {
        public CreatePartCommand(string name )
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}