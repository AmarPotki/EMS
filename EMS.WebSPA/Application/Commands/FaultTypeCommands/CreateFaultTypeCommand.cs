using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands{
    public class CreateFaultTypeCommand : IRequest<bool>
    {
        public CreateFaultTypeCommand(string name )
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}