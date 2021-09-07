using EMS.WebSPA.Application.Commands.ItemTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.ItemType{
    public class DisableItemTypeCommandValidator : AbstractValidator<DisableItemTypeCommand>{
        public DisableItemTypeCommandValidator(){
            RuleFor(x => x.Id).Equal(0).WithMessage("شناسه  نامعتبر است");
        }
    }
}