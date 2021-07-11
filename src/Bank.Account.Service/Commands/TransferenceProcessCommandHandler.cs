using Bank.TransferProcess.Application.Dtos;
using Bank.TransferProcess.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Application.Commands
{
    public class TransferenceProcessCommandHandler :
        IRequestHandler<TransferenceProcessCommand, bool>
    {
        private readonly ITransferProcessService _transferProcessService;
        public TransferenceProcessCommandHandler(ITransferProcessService transferProcessService)
        {
            _transferProcessService = transferProcessService;
        }

        public async Task<bool> Handle(TransferenceProcessCommand message, CancellationToken cancellationToken)
        {
            var transferenceProcessDto = new TransferenceProcessDto(message.Id, message.AccountOrigin, message.AccountDestination, message.Amount);
            var processResult = await _transferProcessService.Process(transferenceProcessDto);
            return processResult;
        }
    }
}
