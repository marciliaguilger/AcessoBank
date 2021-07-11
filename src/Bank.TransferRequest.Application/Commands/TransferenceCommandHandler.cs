using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.TransferRequest.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.TransferRequest.Application.Commands
{
    public class TransferenceCommandHandler :
        IRequestHandler<TransferAmountCommand, TransferAmountDto>
    {
        private readonly ITransferenceService _transferenceService;
        public TransferenceCommandHandler(ITransferenceService transferenceService)
        {
            _transferenceService = transferenceService;
        }
        private bool CommandValidation(TransferAmountCommand message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                //TODO: LANÇAR EVENTO
            }
            return false;
        }
        public async Task<TransferAmountDto> Handle(TransferAmountCommand request, CancellationToken cancellationToken)
        {
            if (!CommandValidation(request)) return null;
            var transference = new Transference(request.Id,
                                                request.AccountOrigin,
                                                request.AccountDestination,
                                                request.Amount,
                                                request.Timestamp,
                                                request.TransferenceStatus);

            _transferenceService.Add(transference);
            transference.AddEvent(new TransferRequestedEvent(request.Id,
                                                                request.AccountOrigin,
                                                                request.AccountDestination,
                                                                request.Amount));
            await _transferenceService.Commit();
            return new TransferAmountDto(request.Id);
        }
    }
}
