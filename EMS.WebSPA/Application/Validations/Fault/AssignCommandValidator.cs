using System;
using EMS.WebSPA.Application.Commands.FaultCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class AssignCommandValidator : AbstractValidator<AssignCommand>
    {
        public AssignCommandValidator(){
            RuleFor(x => x.FaultId).GreaterThan(0).WithMessage(" خرابی معتبر نیست");
            RuleFor(x => x.UserId).Must(ValidateGuid).WithMessage("کاربر معتبر نیست");
        }
        private bool ValidateGuid(string str){
            return Guid.TryParse(str, out var guid) && guid != Guid.Empty;
        }
    }
}