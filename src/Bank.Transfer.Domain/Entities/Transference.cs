using Bank.Transfer.Domain.Core.Entities;
using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.Transfer.Domain.Entities
{
    public class Transference : Entity
    {
        public Transference(Guid id,
                            string accountOrigin, 
                            string accountDestination, 
                            decimal amount, 
                            DateTime requestDate, 
                            TransferenceStatus transferStatus)
        {
            Id = id;
            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Amount = amount;
            RequestDate = requestDate;
            TransferStatus = transferStatus;
            
        }

        public Transference() {}
        public String AccountOrigin{ get; private set; }
        public String AccountDestination { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime? RequestDate { get; private set; }

        public TransferenceStatus TransferStatus { get; private set; }

        public string TransferStatusDetail { get; private  set; }

        public void UpdateStatusDetail(string statusDetail)
        {
            this.TransferStatusDetail = statusDetail;
        }

        public void UpdateStatus(TransferenceStatus status)
        {
            this.TransferStatus = status;
        }

        
    }
}
