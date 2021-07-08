using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.Transaction.Application.Events
{
    public class TransferenceNotFoundEvent : Event
    {
        public Guid Id { get; private set; }
        public TransferenceStatus TransferenceStatus { get; private set; }
        public string TransferStatusDetail { get; private set; }
        public TransferenceNotFoundEvent(Guid id, TransferenceStatus transferenceStatus, string transferStatusDetail)
        {
            Id = id;
            TransferenceStatus = transferenceStatus;
            TransferStatusDetail = transferStatusDetail;
        }
    }
}
