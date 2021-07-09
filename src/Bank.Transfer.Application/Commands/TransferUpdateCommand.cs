using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.Transfer.Application.Events
{
    public class TransferUpdateCommand : Command
    {
        public Guid Id { get; private set; }
        public TransferenceStatus Status { get; private set; }
        public string StatusDetail { get; private set; }
        public TransferUpdateCommand(Guid id, TransferenceStatus status, string statusDetail)
        {
            Id = id;
            Status = status;
            StatusDetail = statusDetail;
        }

    }
}
