using MediatR;
namespace EMS.WebSPA.Application.Commands.LocationCommands{
    public class DisableLocationCommand : IRequest<bool>{
        public DisableLocationCommand(int id){
            Id = id;
        }
        public int Id { get; set; }
    }
}