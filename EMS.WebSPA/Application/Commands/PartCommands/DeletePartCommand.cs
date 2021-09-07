using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class DeletePartCommand : IRequest<bool>{
        public DeletePartCommand(int id){
            Id = id;
        }
        public int Id { get; }
    }
}