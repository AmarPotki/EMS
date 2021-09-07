using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands{
    public class DeleteFaultTypeCommand : IRequest<bool>{
        public DeleteFaultTypeCommand(int id){
            Id = id;
        }
        public int Id { get; }
    }
}