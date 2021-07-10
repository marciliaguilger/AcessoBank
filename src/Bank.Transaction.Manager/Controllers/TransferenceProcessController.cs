using Bank.Transaction.Update.Api.Dtos;
using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bank.Transaction.Manager.Controllers
{
    [ApiController]
    [Route("api/transferenceProcess")]
    public class TransferenceProcessController : ControllerBase
    {
        private readonly ILogger<TransferenceProcessController> _logger;
        private readonly ITransferenceService _transferenceService;
        public TransferenceProcessController(ILogger<TransferenceProcessController> logger, ITransferenceService transferenceService)
        {
            _logger = logger;
            _transferenceService = transferenceService;
        }

        [HttpPost]
        public async Task<bool> Process(TransferRequestedEvent transferRequestedEvent)
        {
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
