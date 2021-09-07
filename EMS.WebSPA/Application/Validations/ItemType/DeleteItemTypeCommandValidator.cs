using EMS.WebSPA.Application.Commands.ItemTypeCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.ItemType{
    public class DeleteItemTypeCommandValidator : AbstractValidator<DeleteItemTypeCommand>{
        public DeleteItemTypeCommandValidator(){
            RuleFor(x => x.Id).Equal(0).WithMessage("شناسه  نامعتبر است");
        }
    }
}