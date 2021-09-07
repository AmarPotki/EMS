using EMS.WebSPA.Application.Commands.FaultTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class DeleteFaultTypeCommandValidator : AbstractValidator<DeleteFaultTypeCommand>{
        public DeleteFaultTypeCommandValidator(){
            RuleFor(x => x.Id).NotEmpty().WithMessage("شناسه نباید خالی باشد");
        }
    }
}