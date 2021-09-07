using EMS.WebSPA.Application.Commands.PartCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Part{
    public class CreatePartCommandValidator:AbstractValidator<CreatePartCommand>{
        public CreatePartCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
        }
    }
}