using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class DisableItemTypeCommand : IRequest<bool>
    {
        public DisableItemTypeCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}