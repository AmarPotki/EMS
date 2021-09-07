using EMS.WebSPA.Application.Commands.FaultTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class CreateFaultTypeCommandValidator : AbstractValidator<CreateFaultTypeCommand>{
        public CreateFaultTypeCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
        }
    }
}