using Bank.TransferRequest.Application.Commands;
using Bank.TransferRequest.Application.Validations;
using System.Linq;
using Xunit;

namespace Bank.TransferRequest.Application.Tests.Commands
{
    public class TransferAmountCommandTests
    {
        [Fact(DisplayName = "Transfer Positive Amount")]
        [Trait("Category", "Transfer Request - Commands")]
        public void TransferAmountCommand_CommandIsValid_ShouldPassValidation()
        {
            var transferAmountCommand = new TransferAmountCommand("89476598", "86744746", 10.0M);
            var result = transferAmountCommand.IsValid();
            Assert.True(result);

        }
        [Fact(DisplayName = "Transfer Negative Amount")]
        [Trait("Category", "Transfer Request - Commands")]
        public void TransferAmountCommand_CommandIsValid_ShouldNotPassValidation()
        {
            var transferAmountCommand = new TransferAmountCommand("89476598", "86744746", -10.0M);
            var result = transferAmountCommand.IsValid();
            Assert.False(result);
            Assert.Contains(TransferAmountCommandValidation.NegativeAmountMessage, transferAmountCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));

        }
    }
}
