using EMS.WebSPA.Application.Commands.FaultCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class AddPartToFaultCommandValidator : AbstractValidator<AddPartToFaultCommand>
    {
        public AddPartToFaultCommandValidator(){
            RuleFor(x => x.FaultId).GreaterThan(0).WithMessage(" خرابی معتبر نیست");
            RuleFor(x => x.PartId).GreaterThan(0).WithMessage("قطعه معتبر نیست");
            RuleFor(x => x.Total).GreaterThan(0).WithMessage("تعداد معتبر نیست");
        }
    }
}