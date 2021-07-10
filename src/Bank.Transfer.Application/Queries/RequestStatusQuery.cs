using Bank.Transfer.Domain.Core.Messages;
using Bank.TransferRequest.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transfer.Application.Queries
{
    public class RequestStatusQuery : IRequest<RequestStatusDto>
    {
        public Guid TransactionId { get; set; }

        public RequestStatusQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
