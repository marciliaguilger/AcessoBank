using Bank.TransferProcess.Application.Dtos;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Application.Interfaces
{
    public interface ITransferProcessService
    {
        Task<bool> Process(TransferenceProcessDto transferenceProcessDto);
    }
}
