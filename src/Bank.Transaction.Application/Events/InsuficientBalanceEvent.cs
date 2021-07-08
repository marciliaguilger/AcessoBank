using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.Transaction.Application.Events
{
    public class InsuficientBalanceEvent : Event
    {
        public Guid Id { get; private set; }
        public TransferenceStatus TransferStatus { get; private set; }
        public string TransferStatusDetail { get; private set; }
        public InsuficientBalanceEvent(Guid id, TransferenceStatus transferStatus, string transferStatusDetail)
        {
            Id = id;
            TransferStatus = transferStatus;
            TransferStatusDetail = transferStatusDetail;
        }
    }
}
