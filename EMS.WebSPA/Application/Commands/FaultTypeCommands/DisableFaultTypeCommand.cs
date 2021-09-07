using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultTypeCommands{
    public class DisableFaultTypeCommand : IRequest<bool>{
        public DisableFaultTypeCommand(int id){
            Id = id;
        }
        public int Id { get; }
    }
}