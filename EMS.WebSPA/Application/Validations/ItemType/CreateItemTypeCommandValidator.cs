using EMS.WebSPA.Application.Commands.ItemTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.ItemType{
    public class CreateItemTypeCommandValidator : AbstractValidator<CreateItemTypeCommand>{
        public CreateItemTypeCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.ParentId).Equal(0).WithMessage("شناسه پدر نامعتبر است");
        }
    }
}