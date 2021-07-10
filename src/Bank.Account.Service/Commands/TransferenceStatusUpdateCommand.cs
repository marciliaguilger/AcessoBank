using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Enums;
using System;

namespace Bank.TransferProcess.Application.Commands
{
    public class TransferenceStatusUpdateCommand : Command<bool>
    {
        public Guid Id { get; private set; }
        public TransferenceStatus TransferenceStatus { get; private set; }
        public string StatusDetail { get; private set; }
        public TransferenceStatusUpdateCommand(Guid id, TransferenceStatus transferenceStatus)
        {
            Id = id;
            TransferenceStatus = transferenceStatus;
        }

        public void SetStatusDetail(string statusDetail)
        {
            StatusDetail = statusDetail;
        }
    }
}
