using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class EnableItemTypeCommand : IRequest<bool>{
        public EnableItemTypeCommand(int id){
            Id = id;
        }
        public int Id { get; set; }
    }
}