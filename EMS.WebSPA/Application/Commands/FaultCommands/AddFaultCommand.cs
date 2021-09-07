using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands
{
    public class AddFaultCommand : IRequest<bool>
    {
        public AddFaultCommand(string title, string description, int itemTypeId, int faultTypeId, int locationId)
        {
            Title = title;
            Description = description;
            ItemTypeId = itemTypeId;
            FaultTypeId = faultTypeId;
            LocationId = locationId;
        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ItemTypeId { get; private set; }
        public int FaultTypeId { get; private set; }
        public int LocationId { get; private set; }

    }
}