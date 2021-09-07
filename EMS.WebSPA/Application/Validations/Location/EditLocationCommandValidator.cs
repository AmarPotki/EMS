using EMS.WebSPA.Application.Commands.LocationCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Location{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>{
        public UpdateLocationCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.Id).Equal(0).WithMessage("شناسه  نامعتبر است");
        }
    }
}