using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Dtos;
using Bank.Transfer.Domain.Core.Interface;
using Bank.TransferProcess.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.TransferProcess.Application.Service
{
    public class TransferProcessService : ITransferProcessService
    {
        private readonly IAccountService _accountService;
        private readonly IMediatorHandler _mediatorHandler;
        public TransferProcessService(IAccountService accountService,
                                        IMediatorHandler mediatorHandler)
        {
            _accountService = accountService;
            _mediatorHandler = mediatorHandler;
        }

        public bool AccountTransaction(string Account, decimal Amount, string type)
        {
            throw new NotImplementedException();
        }

        private UserAccount GetAccountAndBalance(string Account)
        {

        }
        private bool ValidateAccount(string Account)
        {

        }
        private bool ValidateFunder(string Account, decimal Amount)
        {

        }
        private void UpdateStatus(string Account, TransferenceStatus status, string statusDetail)
        {

        }
    }
}
