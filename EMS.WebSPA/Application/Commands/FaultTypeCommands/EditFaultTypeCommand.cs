using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands
{
    public class EditFaultTypeCommand : IRequest<bool>{
        public EditFaultTypeCommand(int id, string name){
            Name = name;
            Id = id;
        }
        public int Id { get; }
        public string Name { get; }
    }
}