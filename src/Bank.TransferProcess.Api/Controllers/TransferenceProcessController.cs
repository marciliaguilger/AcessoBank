using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.TransferProcess.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
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
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Process(TransferRequestedEvent transferRequestedEvent)
        {
            
            var transferenceProcessCommand = new TransferenceProcessCommand(transferRequestedEvent.Id, 
                                                transferRequestedEvent.AccountOrigin,
                                                transferRequestedEvent.AccountDestination,
                                                transferRequestedEvent.Amount);
            
            var result = await _mediatorHandler.SendCommand<TransferenceProcessCommand, bool>(transferenceProcessCommand);

            if (!result) return BadRequest(false);

            return Ok(true);
            
        }
       
    }
}
