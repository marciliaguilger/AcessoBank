using Bank.Transfer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transfer.Application.Events
{
    public class TransferUpdatedEvent
    {
        public Guid Id { get; private set; }
        public TransferenceStatus TransferStatus { get; private set; }
        public string TransferStatusDetail { get; private set; }
        public TransferUpdatedEvent(Guid id, TransferenceStatus transferStatus, string transferStatusDetail)
        {
            Id = id;
            TransferStatus = transferStatus;
            TransferStatusDetail = transferStatusDetail;
        }

    }
}
