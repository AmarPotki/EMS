using EMS.WebSPA.Application.Commands.LocationCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Location{
    public class EnableLocationCommandValidator : AbstractValidator<EnableLocationCommand>
    {
        public EnableLocationCommandValidator(){
            RuleFor(x => x.Id).Equal(0).WithMessage("شناسه نامعتبر است");
        }
    }
}