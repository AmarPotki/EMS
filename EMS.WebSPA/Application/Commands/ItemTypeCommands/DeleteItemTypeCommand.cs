using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class DeleteItemTypeCommand : IRequest<bool>{
        public DeleteItemTypeCommand(int id){
            Id = id;
        }
        public int Id { get; }
    }
}