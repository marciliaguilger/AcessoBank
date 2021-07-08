using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Infrastructure.Context;

namespace Bank.Transfer.Infrastructure.Repository
{
    public class TransferenceRepository : Repository<Transference>, ITransferenceRepository
    {
        public TransferenceRepository(BankContext context) : base(context)
        {
        }
    }
}
