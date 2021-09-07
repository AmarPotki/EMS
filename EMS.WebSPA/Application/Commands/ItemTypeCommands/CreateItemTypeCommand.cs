using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class CreateItemTypeCommand:IRequest<bool>{
        public CreateItemTypeCommand(string name, int? parentId){
            Name = name;
            ParentId = parentId;
        }
        public string Name { get; private set; }
        public int? ParentId { get; private set; }
    }
}