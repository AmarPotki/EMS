using EMS.WebSPA.Application.Commands.ItemTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.ItemType{
    public class EditItemTypeCommandValidator : AbstractValidator<EditItemTypeCommand>
    {
        public EditItemTypeCommandValidator(){
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نباید خالی باشد");
            RuleFor(x => x.Id).Equal(0).WithMessage("شناسه  نامعتبر است");
        }
    }
}