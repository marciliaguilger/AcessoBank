using Bank.Transfer.Application.Validations;
using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Enums;
using Bank.TransferRequest.Application.Dtos;
using MediatR;
using System;

namespace Bank.Transfer.Application.Commands
{
    public class TransferAmountCommand : Command<TransferAmountDto>
    //public class TransferAmountCommand : IRequest<TransferAmountDto>
    {
        public Guid Id { get; private set; }
        public String AccountOrigin { get; private set; }
        public String AccountDestination { get; private set; }
        public decimal Amount { get; private set; }
        public TransferenceStatus TransferenceStatus { get; private set; }
        public TransferAmountCommand(string accountOrigin, string accountDestination, decimal amount)
        {
            Id = Guid.NewGuid();
            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Amount = amount;
            TransferenceStatus = TransferenceStatus.InQueue;
        }

        public override bool IsValid()
        {
            ValidationResult = new TransferAmountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}
