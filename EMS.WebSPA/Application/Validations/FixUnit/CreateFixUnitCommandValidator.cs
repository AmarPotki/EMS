using System;
using EMS.WebSPA.Application.Commands.FixUnitCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.FixUnit{
    public class CreateFixUnitCommandValidator : AbstractValidator<CreateFixUnitCommand>{
        public CreateFixUnitCommandValidator(){
            RuleFor(x => x.Title).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.Description).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.LocationId).GreaterThan(0).WithMessage("شناسه مکان نامعتبر است");
            RuleFor(x => x.ItemTypeId).GreaterThan(0).WithMessage("شناسه نوع نامعتبر است");
            RuleFor(x => x.Owner).Must(ValidateGuid).WithMessage("کاربر معتبر نیست");
        }
        private bool ValidateGuid(string str){
            return Guid.TryParse(str, out var guid) && guid != Guid.Empty;
        }
    }
}