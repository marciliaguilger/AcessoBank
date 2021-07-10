using Bank.Transfer.Application.Events;
using Bank.Transfer.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Transaction.Application.Services
{
    public interface ITransactionAppService
    {
        void ProccessTransferenceAsync(TransferRequestedEvent transference);
    }
}
