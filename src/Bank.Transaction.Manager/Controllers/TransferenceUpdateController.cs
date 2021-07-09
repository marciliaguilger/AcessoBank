using Bank.Transaction.Update.Api.Dtos;
using Bank.Transfer.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bank.Transaction.Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenceUpdateController : ControllerBase
    {
        private readonly ILogger<TransferenceUpdateController> _logger;
        private readonly ITransferenceService _transferenceService;
        public TransferenceUpdateController(ILogger<TransferenceUpdateController> logger, ITransferenceService transferenceService)
        {
            _logger = logger;
            _transferenceService = transferenceService;
        }

        [HttpPost]
        [Route("update-status")]
        public async Task<bool> Update(TransferenceUpdateDto transferenceDto)
        {
            var transference = _transferenceService.GetById(transferenceDto.Id);
            transference.UpdateStatus(transferenceDto.Status);
            transference.UpdateStatusDetail(transferenceDto.StatusDetail);
            return await _transferenceService.UpdateAsync(transference);
        }
    }
}
