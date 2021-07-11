using Bank.Transfer.Domain.Core.Events;
using Refit;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Core.Interface
{
    public interface ITransferenceProcessService
    {
        [Post("/api/transferenceProcess")]
        Task<ApiResponse<bool>> ProcessTransferenceRequest(TransferRequestedEvent transferRequestedEvent);
    }
}
