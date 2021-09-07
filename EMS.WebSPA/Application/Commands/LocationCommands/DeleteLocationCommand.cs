using MediatR;

namespace EMS.WebSPA.Application.Commands.LocationCommands
{
    public class DeleteLocationCommand:IRequest<bool>
    {
        public DeleteLocationCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}