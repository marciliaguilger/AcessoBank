using Bank.Transfer.Domain.Core.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Transfer.Domain.Core.Interface
{
    public interface IAccountService
    {
        [Get("/api/Account/{accountNumber}")]
        Task<ApiResponse<UserAccount>> GetAccountAndBalance(string accountNumber);

        [Post("/api/Account")]
        Task<HttpResponseMessage> AccountTrasaction(TransferenceRequest transferenceRequest);

    }
}
