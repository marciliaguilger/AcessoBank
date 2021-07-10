﻿using Bank.Transfer.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transfer.Domain.Core.Events
{
    public class TransferRequestedEvent : Event
    {
        public TransferRequestedEvent() {}
        public TransferRequestedEvent(Guid id,
                                        string accountOrigin,
                                        string accountDestination,
                                        decimal amount)
        {
            Id = id;
            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Amount = amount;
        }

        public Guid Id { get; private set; }
        public String AccountOrigin { get; private set; }
        public String AccountDestination { get; private set; }
        public decimal Amount { get; private set; }

    }
}
