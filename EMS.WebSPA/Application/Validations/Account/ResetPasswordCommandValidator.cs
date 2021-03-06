using System;
using EMS.WebSPA.Application.Commands.AccountCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Account{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>{
        public ResetPasswordCommandValidator(){
            RuleFor(x => x.UserIdentity).Must(ValidateGuid).WithMessage("کاربر معتبر نیست");
        }
        private bool ValidateGuid(string str){
            return Guid.TryParse(str, out var guid) && guid != Guid.Empty;
        }
    }
}