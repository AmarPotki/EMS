using MediatR;
namespace EMS.WebSPA.Application.Commands.LocationCommands{
    public class EnableLocationCommand : IRequest<bool>{
        public EnableLocationCommand(int id){
            Id = id;
        }
        public int Id { get; set; }
    }
}