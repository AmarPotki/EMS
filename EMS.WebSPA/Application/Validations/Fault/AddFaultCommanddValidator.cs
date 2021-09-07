using EMS.WebSPA.Application.Commands.FaultCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class AddFaultCommanddValidator : AbstractValidator<AddFaultCommand>{
        public AddFaultCommanddValidator(){
            RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان نباید خالی باشد");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد");
            RuleFor(x => x.ItemTypeId).GreaterThan(0).WithMessage("نوع خرابی معتبر نیست");
            RuleFor(x => x.LocationId).GreaterThan(0).WithMessage("مکان معتبر نیست");
            RuleFor(x => x.FaultTypeId).GreaterThan(0).WithMessage("نوع معتبر نیست");
        }
    }
}