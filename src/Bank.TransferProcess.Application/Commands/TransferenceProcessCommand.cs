using Bank.Transfer.Domain.Core.Messages;
using System;

namespace Bank.TransferProcess.Application.Commands
{
    public class TransferenceProcessCommand : Command<bool>
    {
        public Guid Id { get; private set; }
        public String AccountOrigin { get; private set; }
        public String AccountDestination { get; private set; }
        public decimal Amount { get; private set; }
        public TransferenceProcessCommand(Guid id, string accountOrigin, string accountDestination, decimal amount)
        {
            Id = id;
            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Amount = amount;
        }
    }
}
