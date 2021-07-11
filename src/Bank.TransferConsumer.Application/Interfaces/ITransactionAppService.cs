using Bank.Transfer.Domain.Core.Events;

namespace Bank.TransferConsumer.Application.Interfaces
{
    public interface ITransactionAppService
    {
        void ProccessTransferenceAsync(TransferRequestedEvent transference);
    }
}
