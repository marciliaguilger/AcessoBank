using Bank.Transfer.Domain.Core.Communication;
using Bank.TransferRequest.Application.Commands;
using Bank.TransferRequest.Application.Dtos;
using Bank.TransferRequest.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TransferenceController> _logger;

        public TransferenceController(IMediatorHandler mediatorHandler, ILogger<TransferenceController> logger)
        {
            _mediatorHandler = mediatorHandler;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Transfer(TransferenceDto transferenceDto)
        {
            var requestData = JsonConvert.SerializeObject(transferenceDto);
            try
            {
                _logger.LogInformation($"New transfer requested: {requestData}");
                var command = new TransferAmountCommand(transferenceDto.AccountOrigin, 
                                                        transferenceDto.AccountDestination,
                                                        transferenceDto.Amount);
                var transferAmountDto = await _mediatorHandler.SendCommand<TransferAmountCommand, TransferAmountDto>(command);
                if (transferAmountDto == null)
                {
                    _logger.LogError($"Bad request ocurred: {requestData}");
                    return BadRequest();
                }
                return Ok(JsonConvert.SerializeObject(transferAmountDto));
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error when transfer requested {requestData}");
                return Problem(ex.ToString());
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RequestStatusDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"New query of transference id {id}");
            try
            {
                var requestStatusQuery = new RequestStatusQuery(id);
                var requestStatusDto = await _mediatorHandler.SendCommand<RequestStatusQuery, RequestStatusDto>(requestStatusQuery);
                if (requestStatusDto == null)
                {
                    _logger.LogInformation($"Transference id {id} not found");
                    return NoContent();
                }
                return Ok(JsonConvert.SerializeObject(requestStatusDto));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error when query of transference id {id}");
                return Problem();
            }
        }
    }
}
