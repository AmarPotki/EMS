using EMS.WebSPA.Application.Commands.FaultCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class FixFaultCommandValidator : AbstractValidator<FixFaultCommand>
    {
        public FixFaultCommandValidator(){
            RuleFor(x => x.FaultId).GreaterThan(0).WithMessage(" خرابی معتبر نیست");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد ");
        }
    }
}