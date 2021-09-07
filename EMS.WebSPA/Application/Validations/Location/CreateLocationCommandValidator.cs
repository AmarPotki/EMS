using EMS.WebSPA.Application.Commands.LocationCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Location{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>{
        public CreateLocationCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.ParentId).Equal(0).WithMessage("شناسه پدر نامعتبر است");
        }
    }
}