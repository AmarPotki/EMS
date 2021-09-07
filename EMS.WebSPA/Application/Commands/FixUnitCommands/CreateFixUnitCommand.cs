using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class CreateFixUnitCommand : IRequest<bool>{
        public CreateFixUnitCommand(int locationId, int itemTypeId, string owner, string[] members, string title,
            string description){
            LocationId = locationId;
            ItemTypeId = itemTypeId;
            Owner = owner;
            Members = members;
            Title = title;
            Description = description;
        }
        public int LocationId { get; }
        public int ItemTypeId { get; }
        public string Owner { get; }
        public string[] Members { get; }
        public string Title { get; }
        public string Description { get; }
    }
}