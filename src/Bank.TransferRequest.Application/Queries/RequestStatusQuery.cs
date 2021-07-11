using Bank.Transfer.Domain.Core.Messages;
using Bank.TransferRequest.Application.Dtos;
using System;

namespace Bank.TransferRequest.Application.Queries
{
    public class RequestStatusQuery : Command<RequestStatusDto>
    {
        public Guid TransactionId { get; set; }

        public RequestStatusQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
