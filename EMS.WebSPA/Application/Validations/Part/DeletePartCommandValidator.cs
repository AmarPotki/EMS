using EMS.WebSPA.Application.Commands.PartCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Part{
    public class DeletePartCommandValidator : AbstractValidator<DeletePartCommand>{
        public DeletePartCommandValidator(){
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است");
        }
    }
}