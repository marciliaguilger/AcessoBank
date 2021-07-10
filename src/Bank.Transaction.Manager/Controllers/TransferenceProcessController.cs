using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.TransferProcess.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Api.Controllers
{
    [ApiController]
    [Route("api/transferenceProcess")]
    public class TransferenceProcessController : ControllerBase
    {
        private readonly ILogger<TransferenceProcessController> _logger;
        private readonly ITransferenceService _transferenceService;
        private readonly IMediatorHandler _mediatorHandler;
        public TransferenceProcessController(ILogger<TransferenceProcessController> logger,
                                            ITransferenceService transferenceService,
                                            IMediatorHandler mediatorHandler)
        {
            _logger = logger;
            _transferenceService = transferenceService;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<bool> Process(TransferRequestedEvent transferRequestedEvent)
        {
            var transferenceProcessCommand = new TransferenceProcessCommand(transferRequestedEvent.Id, 
                                                transferRequestedEvent.AccountOrigin,
                                                transferRequestedEvent.AccountDestination,
                                                transferRequestedEvent.Amount);
            
            await _mediatorHandler.SendCommand<TransferenceProcessCommand, bool>(transferenceProcessCommand);


            //var transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(transferRequestedEvent.Id, TransferenceStatus.Processing);
            //await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);

            //var transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(transferRequestedEvent.Id, TransferenceStatus.Confirmed);
            //await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);


            var teste = transferRequestedEvent;
            return true;

            //var transference = _transferenceService.GetById(transferenceDto.Id);
            //transference.UpdateStatus(transferenceDto.Status);
            //transference.UpdateStatusDetail(transferenceDto.StatusDetail);
            //return await _transferenceService.UpdateAsync(transference);
        }
        //[HttpPost]
        //[Route("update-status")]
        //public async Task<bool> Update(TransferenceUpdateDto transferenceDto)
        //{
        //    var transference = _transferenceService.GetById(transferenceDto.Id);
        //    transference.UpdateStatus(transferenceDto.Status);
        //    transference.UpdateStatusDetail(transferenceDto.StatusDetail);
        //    return await _transferenceService.UpdateAsync(transference);
        //}
    }
}
