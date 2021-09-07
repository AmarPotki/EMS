using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class EditPartCommand : IRequest<bool>{
        public EditPartCommand(int id, string name){
            Name = name;
            Id = id;
        }
        public int Id { get; }
        public string Name { get; }
    }
}