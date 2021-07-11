using System;

namespace Bank.TransferProcess.Application.Dtos
{
    public class TransferenceProcessDto
    {
        public TransferenceProcessDto(Guid id, string accountOrigin, string accountDestination, decimal amount)
        {
            Id = id;
            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Amount = amount;
        }

        public Guid Id { get; set; }
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public decimal Amount { get; set; }

        

    }
}
