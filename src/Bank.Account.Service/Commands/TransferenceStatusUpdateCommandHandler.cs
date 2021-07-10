using Bank.Transfer.Domain.Enums;
using Bank.Transfer.Domain.Interfaces.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Application.Commands
{
    public class TransferenceStatusUpdateCommandHandler :
        IRequestHandler<TransferenceStatusUpdateCommand, bool>
    {
        private readonly ITransferenceService _transferenceService;

        public TransferenceStatusUpdateCommandHandler(ITransferenceService transferenceService)
        {
            _transferenceService = transferenceService;
        }
        public async Task<bool> Handle(TransferenceStatusUpdateCommand message, CancellationToken cancellationToken)
        {
            await _transferenceService.UpdateStatus(message.Id, TransferenceStatus.Processing);
            return true;
        }
    }
}

