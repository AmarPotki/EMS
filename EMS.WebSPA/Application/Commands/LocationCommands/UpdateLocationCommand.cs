using MediatR;

namespace EMS.WebSPA.Application.Commands.LocationCommands
{
    public class UpdateLocationCommand:IRequest<bool>
    {
        public UpdateLocationCommand(int id,string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get;  }
        public string  Name{ get; }

    }
}