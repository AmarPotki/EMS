using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebSPA.Application.Commands.FixUnitCommands
{
    public class DeleteFixUnitCommand:IRequest<bool>
    {
        public DeleteFixUnitCommand(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
    }
}
