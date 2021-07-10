using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Enums;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using System;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Services
{
    public class TransferenceService : Service<Transference>, ITransferenceService
    {
        public TransferenceService(ITransferenceRepository transferenceRepository) 
            : base(transferenceRepository)
        {
        }


        public async Task UpdateStatus(Guid id, TransferenceStatus transferenceStatus, string statusDetail)
        {
            var transference = GetById(id);
            if (transference != null)
            {
                transference.UpdateStatus(transferenceStatus);
                if (!string.IsNullOrEmpty(statusDetail))
                    transference.UpdateStatusDetail(statusDetail);

                await UpdateAsync(transference);
            }
        }
    }
}
