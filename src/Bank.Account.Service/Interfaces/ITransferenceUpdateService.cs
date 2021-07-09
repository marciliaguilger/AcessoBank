using Bank.Account.Service.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Account.Service.Interfaces
{
    public interface ITransferenceUpdateService
    {
        [Post("/api/TransferenceUpdate/update-status")]
        Task<HttpResponseMessage> UpdateTansferenceStatus(TransferenceToUpdateRequest transferenceToUpdateRequest);
    }
}
