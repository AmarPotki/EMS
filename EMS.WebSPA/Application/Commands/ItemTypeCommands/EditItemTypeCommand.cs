using MediatR;
namespace EMS.WebSPA.Application.Commands.ItemTypeCommands{
    public class EditItemTypeCommand : IRequest<bool>
    {
        public EditItemTypeCommand(string name, int id)
        {
            Name = name;
            Id = id;
        }
        public string Name { get; private set; }
        public int Id { get; private set; }
    }
}