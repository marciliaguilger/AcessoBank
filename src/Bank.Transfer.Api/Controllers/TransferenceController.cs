using Bank.Transfer.Application.Commands;
using Bank.Transfer.Application.Dtos;
using Bank.Transfer.Application.Events;
using Bank.Transfer.Application.Interfaces;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace Bank.Transfer.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TransferenceController : ControllerBase
    {
        
        //private readonly ILogger<TransferenceController> _logger;
        private readonly ITransferenceAppService _transferenceAppService;
        private readonly IMediatorHandler _mediatorHandler;

        public TransferenceController(ITransferenceAppService transferenceAppService,
                                        IMediatorHandler mediatorHandler)
        {
            _transferenceAppService = transferenceAppService;
            _mediatorHandler = mediatorHandler;
        }


        //public TransferenceController(ILogger<TransferenceController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpPost]
        [Route("fund-transfer")]
        public async Task<IActionResult> Transfer(TransferenceDto transferenceDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var command = new TransferAmountCommand(transferenceDto.AccountOrigin, 
                                                    transferenceDto.AccountDestination,
                                                    transferenceDto.Amount);
            await _mediatorHandler.SendCommand(command);

            return Ok(command.Id);
        }

        [HttpPost]
        [Route("transfer-update")]
        public async Task<IActionResult> Update(TransferenceUpdateDto transferenceDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var command = new TransferUpdateCommand(transferenceDto.Id,
                                                    transferenceDto.Status,
                                                    transferenceDto.StatusDetail);
            await _mediatorHandler.SendCommand(command);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var transference = _transferenceAppService.GetById(id);
            return Ok(transference);

        }
    }
}
