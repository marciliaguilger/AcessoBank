using Bank.TransferRequest.Application.Commands;
using FluentValidation;

namespace Bank.TransferRequest.Application.Validations
{

    public class TransferAmountCommandValidation : AbstractValidator<TransferAmountCommand>
    {
        public static string NegativeAmountMessage => "Amount can not be zero";

        public TransferAmountCommandValidation()
        {
            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage("Amount can not be zero");
        }
    }
}
