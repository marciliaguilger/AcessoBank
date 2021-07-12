using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Core.Interface;
using Bank.TransferConsumer.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Bank.TransferConsumer.Application.Services
{
    public class TransferenceConsumerAppService : ITransactionAppService
    {
        private readonly ITransferenceProcessService _transferenceProcessService;
        private readonly ILogger<TransferenceConsumerAppService> _logger;

        public TransferenceConsumerAppService(ITransferenceProcessService transferenceProcessService, ILogger<TransferenceConsumerAppService> logger)
        {
            _transferenceProcessService = transferenceProcessService;
            _logger = logger;
        }
        public  async void ProccessTransferenceAsync(TransferRequestedEvent transferRequestedEvent)
        {
            try
            {
                var apiResponse = await _transferenceProcessService.ProcessTransferenceRequest(transferRequestedEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when trying to process transference request");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
