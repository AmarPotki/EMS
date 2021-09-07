using EMS.WebSPA.Application.Commands.PartCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Part{
    public class EditPartCommandCommandValidator : AbstractValidator<EditPartCommand>
    {
        public EditPartCommandCommandValidator(){
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است");
        }
    }
}