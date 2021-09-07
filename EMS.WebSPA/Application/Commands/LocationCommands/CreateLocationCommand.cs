using MediatR;
namespace EMS.WebSPA.Application.Commands.LocationCommands{
    public class CreateLocationCommand:IRequest<bool>{
        public CreateLocationCommand(string name, int? parentId){
            Name = name;
            ParentId = parentId;
        }
        public string Name { get; private set; }
        public int? ParentId { get; private set; }
    }
}