using EMS.WebSPA.Application.Commands.FaultCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class MoveFaultCommandValidator : AbstractValidator<MoveFaultCommand>{
        public MoveFaultCommandValidator(){
            RuleFor(x => x.FaultId).GreaterThan(0).WithMessage(" خرابی معتبر نیست");
            RuleFor(x => x.FixUnitId).GreaterThan(0).WithMessage("واحد رفع کننده معتبر نیست");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد ");
        }
    }
}