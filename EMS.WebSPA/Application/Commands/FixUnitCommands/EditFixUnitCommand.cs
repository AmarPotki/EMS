using MediatR;

namespace EMS.WebSPA.Application.Commands.FixUnitCommands
{
    public class EditFixUnitCommand : IRequest<bool>
    {
        public EditFixUnitCommand(int id,string description, string title, int itemTypeId, int locationId, string owner)
        {
            Description = description;
            Title = title;
            ItemTypeId = itemTypeId;
            LocationId = locationId;
            Owner = owner;
            Id = id;
        }

        public int Id  { get; set; }
        public int ItemTypeId { get; private set; }
        public string Owner { get; private set; }
        public int LocationId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    }
}
