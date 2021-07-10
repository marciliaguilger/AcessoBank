using Bank.Transfer.Application.Events;
using Bank.Transfer.Domain.Core.Messages;
using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Enums;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.TransferRequest.Application.Dtos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Transfer.Application.Commands
{
    public class TransferenceCommandHandler :
        //IRequestHandler<TransferAmountCommand, bool>
        IRequestHandler<TransferAmountCommand, TransferAmountDto>
    {
        private readonly ITransferenceService _transferenceService;
        public TransferenceCommandHandler(ITransferenceService transferenceService)
        {
            _transferenceService = transferenceService;
        }
        //public async Task<bool> Handle(TransferAmountCommand message, CancellationToken cancellationToken)
        //{
        //    if (!CommandValidation(message)) return false;
        //    var transference = new Transference(message.Id,
        //                                        message.AccountOrigin, 
        //                                        message.AccountDestination, 
        //                                        message.Amount,
        //                                        message.Timestamp,
        //                                        message.TransferenceStatus);

        //    _transferenceService.Add(transference);
        //    transference.AddEvent(new TransferRequestedEvent(message.Id,
        //                                                        message.AccountOrigin,
        //                                                        message.AccountDestination,
        //                                                        message.Amount));

        //    return await _transferenceService.Commit();
        //}
        //public async Task<bool> Handle(TransferUpdateCommand message, CancellationToken cancellationToken)
        //{
        //    if (!CommandValidation(message)) return false;

        //    var transference = _transferenceService.GetById(message.Id);
        //    transference.UpdateStatus(message.Status);
        //    transference.UpdateStatusDetail(message.StatusDetail);
        //    _transferenceService.Update(transference);

        //    return await _transferenceService.Commit();
        //}

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
        //public async Task<TransferAmountDto> IRequestHandler<TransferAmountCommand, TransferAmountDto>.Handle(TransferAmountCommand request, CancellationToken cancellationToken)
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
