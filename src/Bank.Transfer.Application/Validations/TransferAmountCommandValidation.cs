using Bank.Transfer.Application.Commands;
using FluentValidation;

namespace Bank.Transfer.Application.Validations
{

    public class TransferAmountCommandValidation : AbstractValidator<TransferAmountCommand>
    {

        public TransferAmountCommandValidation()
        {
            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage("Amount can not be zero");
        }
    }
}
