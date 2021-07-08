using Bank.Account.Service.Dtos;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bank.Account.Service.Interfaces
{
    public interface IAccountService
    {
        [Get("/api/Account/{accountNumber}")]
        Task<ApiResponse<UserAccount>> GetAccountAndBalance(string accountNumber);

        [Post("/api/Account")]
        Task<HttpResponseMessage> AccountTrasaction(TransferenceRequest transferenceRequest);

    }
}
