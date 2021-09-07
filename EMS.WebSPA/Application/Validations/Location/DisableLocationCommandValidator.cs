using EMS.WebSPA.Application.Commands.LocationCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Location{
    public class DisableLocationCommandValidator : AbstractValidator<DisableLocationCommand>{
        public DisableLocationCommandValidator(){
            RuleFor(x => x.Id).Equal(0).WithMessage("شناسه نامعتبر است");
        }
    }
}