using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Interfaces.Service
{
    public interface ITransferenceService : IService<Transference>
    {
        Task UpdateStatus(Guid id, TransferenceStatus transferenceStatus, string statusDetail = "");
    }
}
