using Bank.Transaction.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bank.Transaction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITransactionAppService _transactionAppService;
        public AccountController(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }

        [HttpGet]
        [Route("Start")]
        public async Task<IActionResult> Index()
        {
             //await _transactionAppService.ListenMessages();
            return NoContent();
        }
    }
}