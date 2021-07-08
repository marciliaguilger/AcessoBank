using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;

namespace Bank.Transfer.Domain.Services
{
    public class TransferenceService : Service<Transference>, ITransferenceService
    {
        private readonly ITransferenceRepository _transferenceRepository;

        public TransferenceService(ITransferenceRepository transferenceRepository) 
            : base(transferenceRepository)
        {
            _transferenceRepository = transferenceRepository;
        }

    }
}
