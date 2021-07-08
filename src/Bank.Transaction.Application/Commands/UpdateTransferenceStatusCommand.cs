using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.Transaction.Application.Commands
{
    public class UpdateTransferenceStatusCommand : Command
    {
        public Guid Id { get; private set; }
        public TransferenceStatus TransferenceStatus { get; private set; } //TODO: TROCAR O NOME DO OBJETO ENUM
        public UpdateTransferenceStatusCommand(Guid id, TransferenceStatus transferenceStatus)
        {
            Id = id;
            TransferenceStatus = transferenceStatus;
        }
    }
}
