using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;

namespace Bank.Transfer.Domain.Services
{
    public class TransferenceService : Service<Transference>, ITransferenceService
    {
        public TransferenceService(ITransferenceRepository transferenceRepository) 
            : base(transferenceRepository)
        {
        }

    }
}
