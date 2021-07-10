using Newtonsoft.Json;
using System;

namespace Bank.TransferRequest.Application.Dtos
{
    public class TransferAmountDto
    {
        [JsonProperty("transactionId")]
        public Guid TransactionId { get; private set; }

        public TransferAmountDto(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
