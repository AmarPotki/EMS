using EMS.WebSPA.Application.Commands.FaultCommands;
using FluentValidation;
namespace EMS.WebSPA.Application.Validations.Fault{
    public class DeletePartFromFaultCommandValidator : AbstractValidator<DeletePartFromFaultCommand>
    {
        public DeletePartFromFaultCommandValidator(){
            RuleFor(x => x.FaultId).GreaterThan(0).WithMessage(" خرابی معتبر نیست");
            RuleFor(x => x.PartId).GreaterThan(0).WithMessage("قطعه معتبر نیست");
        }
    }
}