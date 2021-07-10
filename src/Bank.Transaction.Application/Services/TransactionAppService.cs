//using Bank.Transaction.Application.Commands;
using Bank.Transfer.Application.Events;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Core.Interface;
using Bank.Transfer.Domain.Enums;
using MediatR;
using System;
using System.Diagnostics;

namespace Bank.Transaction.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {


        private readonly ITransferenceProcessService _transferenceProcessService;

        public TransactionAppService(ITransferenceProcessService transferenceProcessService)
        {
            _transferenceProcessService = transferenceProcessService;
        }
        

        public  async void ProccessTransferenceAsync(TransferRequestedEvent transferRequestedEvent)
        {
            try
            {
                var apiResponse = await _transferenceProcessService.ProcessTransferenceRequest(transferRequestedEvent);
                var teste = "teste";
                //apiResponse.IsSuccessStatusCode;

                //var updateCommand = new UpdateTransferenceStatusCommand(transference.Id, TransferenceStatus.Processing);

                //await _mediatorHandler.SendCommand<UpdateTransferenceStatusCommand, bool>(updateCommand);

                //var command = new ProcessTransferenceCommand(
                //                                        transference.Id,
                //                                        transference.AccountDestination,
                //                                        transference.AccountOrigin,
                //                                        transference.Amount);

                //await _mediatorHandler.SendCommand<ProcessTransferenceCommand, bool>(command);

            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
