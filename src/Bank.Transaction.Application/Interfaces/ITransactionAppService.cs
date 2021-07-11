using Bank.Transfer.Domain.Core.Events;

namespace Bank.Transaction.Application.Interfaces
{
    public interface ITransactionAppService
    {
        void ProccessTransferenceAsync(TransferRequestedEvent transference);
    }
}
