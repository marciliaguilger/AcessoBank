using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.Account.Service.Dtos
{
    public class TransferenceToUpdateRequest
    {
        public TransferenceToUpdateRequest(Guid id, TransferenceStatus status, string statusDetail="")
        {
            Id = id;
            Status = status;
            StatusDetail = statusDetail;
        }


        public Guid Id { get; set; }
        public TransferenceStatus Status { get; set; }
        public string StatusDetail { get; set; }
    }
}
