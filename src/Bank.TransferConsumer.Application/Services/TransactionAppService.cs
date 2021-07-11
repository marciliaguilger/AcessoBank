using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Core.Interface;
using Bank.TransferConsumer.Application.Interfaces;
using System;
using System.Diagnostics;

namespace Bank.TransferConsumer.Application.Services
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
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
