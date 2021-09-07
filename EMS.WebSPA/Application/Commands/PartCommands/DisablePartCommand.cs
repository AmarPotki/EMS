using MediatR;
namespace EMS.WebSPA.Application.Commands.PartCommands
{
    public class DisablePartCommand : IRequest<bool>{
        public DisablePartCommand(int id){
            Id = id;
        }
        public int Id { get; }
    }
}