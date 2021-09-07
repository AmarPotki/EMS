using EMS.WebSPA.Application.Commands.FixUnitCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.FixUnit{
    public class DeleteFixUnitCommandValidator : AbstractValidator<DeleteFixUnitCommand>{
        public DeleteFixUnitCommandValidator(){
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه  نامعتبر است");
        }
    }
}