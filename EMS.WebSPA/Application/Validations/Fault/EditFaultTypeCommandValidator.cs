using EMS.WebSPA.Application.Commands.FaultTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class EditFaultTypeCommandValidator : AbstractValidator<EditFaultTypeCommand>{
        public EditFaultTypeCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.Id).NotEmpty().WithMessage("شناسه نباید خالی باشد");
        }
    }
}