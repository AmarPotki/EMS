using EMS.WebSPA.Application.Commands.AccountCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Account{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(){
            RuleFor(x => x.FullName).NotEmpty().WithMessage("نام کامل نباید خالی باشد ");
            RuleFor(x => x.Password).NotEmpty().WithMessage("کلمه عبور نباید خالی باشد ");
            RuleFor(x => x.Role).NotEmpty().WithMessage("نقش نباید خالی باشد ");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("نام کاربری نباید خالی باشد ");
        }
       
    }
}