using Bank.Transfer.Application.Commands;
using Bank.Transfer.Application.Dtos;
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
        
        //[HttpPost]
        //[Route("transfer-update")]
        //public async Task<bool> Update(TransferenceUpdateDto transferenceDto)
        //{
        //    var transference = _transferenceAppService.GetById(transferenceDto.Id);
        //    transference.UpdateStatus(transferenceDto.Status);
        //    transference.UpdateStatusDetail(transferenceDto.StatusDetail);
        //    return await _transferenceAppService.UpdateAsync(transference);
        //}

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var transference = _transferenceAppService.GetById(id);
            return Ok(transference);

        }
    }
}
