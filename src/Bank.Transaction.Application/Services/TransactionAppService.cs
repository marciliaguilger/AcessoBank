using Bank.Transaction.Application.Commands;
using Bank.Transfer.Application.Events;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Enums;
using MediatR;
using System;
using System.Diagnostics;

namespace Bank.Transaction.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {

        //private readonly IMediator _mediator;
        private readonly IMediatorHandler _mediatorHandler;

        public TransactionAppService(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        

        public  async void ProccessTransferenceAsync(TransferRequestedEvent transference)
        {
            try
            {
                var updateCommand = new UpdateTransferenceStatusCommand(transference.Id, TransferenceStatus.Processing);
                //await _mediatorHandler.SendCommand<bool>(updateCommand);
                await _mediatorHandler.SendCommand<UpdateTransferenceStatusCommand, bool>(updateCommand);

                var command = new ProcessTransferenceCommand(
                                                        transference.Id,
                                                        transference.AccountDestination,
                                                        transference.AccountOrigin,
                                                        transference.Amount);

                //await _mediatorHandler.SendCommand(command);
                await _mediatorHandler.SendCommand<ProcessTransferenceCommand, bool>(command);

                //var result = await _mediator.Send(new ProcessTransferenceCommand(
                //                                        transference.Id,
                //                                        transference.AccountDestination,
                //                                        transference.AccountOrigin,
                //                                        transference.Amount));
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }


        }
    }
}
