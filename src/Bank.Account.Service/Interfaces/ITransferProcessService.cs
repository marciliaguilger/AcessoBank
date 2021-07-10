using Bank.Transfer.Domain.Core.Dtos;
using Bank.Transfer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.TransferProcess.Application.Interfaces
{
    public interface ITransferProcessService
    {
        //deixar os métodos privados na classe concreta
        bool AccountTransaction(string Account, decimal Amount, string type);
        //UserAccount GetAccountAndBalance(string Account);
        //bool ValidateAccount(string Account);
        //bool ValidateFunder(string Account, decimal Amount);
        //void UpdateStatus(string Account, TransferenceStatus status, string statusDetail);
    }
}
