using System;
using EMS.WebSPA.Application.Commands.FixUnitCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.FixUnit{
    public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>{
        public DeleteMemberCommandValidator(){
            RuleFor(x => x.FixUnitId).GreaterThan(0).WithMessage("شناسه نوع نامعتبر است");
            RuleFor(x => x.UserGuid).Must(ValidateGuid).WithMessage("کاربر معتبر نیست");
        }
        private bool ValidateGuid(string str){
            return Guid.TryParse(str, out var guid) && guid != Guid.Empty;
        }
    }
}