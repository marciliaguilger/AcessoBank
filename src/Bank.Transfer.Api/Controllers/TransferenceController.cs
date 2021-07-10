using Bank.Transfer.Application.Commands;
using Bank.Transfer.Application.Dtos;
using Bank.Transfer.Application.Queries;
using Bank.Transfer.Domain.Core.Communication;
using Bank.TransferRequest.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Bank.Transfer.Api.Controllers
{
    [ApiController]
    [Route("api/fund-transfer")]
    public class TransferenceController : ControllerBase
    {
        
        private readonly IMediatorHandler _mediatorHandler;

        public TransferenceController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Transfer(TransferenceDto transferenceDto)
        {
            //if (!ModelState.IsValid) return BadRequest();

            var command = new TransferAmountCommand(transferenceDto.AccountOrigin, 
                                                    transferenceDto.AccountDestination,
                                                    transferenceDto.Amount);
            var transferAmountDto = await _mediatorHandler.SendCommand<TransferAmountCommand, TransferAmountDto>(command);
            if (transferAmountDto == null) return BadRequest();

            return Ok(JsonConvert.SerializeObject(transferAmountDto));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RequestStatusDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            var requestStatusQuery = new RequestStatusQuery(id);
            var requestStatusDto = await _mediatorHandler.SendCommand<RequestStatusQuery, RequestStatusDto>(requestStatusQuery);

            if (requestStatusDto == null) return NoContent();
            return Ok(JsonConvert.SerializeObject(requestStatusDto));
        }
    }
}
