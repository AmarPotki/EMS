using BuildingBlocks.Infrastructure.Services.Commands;
using EMS.WebSPA.Application.Commands.LocationCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations{
    public class CreateLocationCommandValidator{
        public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateLocationCommand, bool>>{
            public IdentifiedCommandValidator(){
                RuleFor(command => command.Id).NotEmpty();
            }
        }
    }
}