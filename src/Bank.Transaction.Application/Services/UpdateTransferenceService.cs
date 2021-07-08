//using Bank.Account.Service.Dtos;
//using Bank.Account.Service.Interfaces;
//using Bank.Transfer.Application.Events;
//using Bank.Transfer.Domain.Entities;
//using Bank.Transfer.Domain.Enums;
//using Bank.Transfer.Domain.Interfaces.Service;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bank.Transaction.Application.Services
//{
//    public class UpdateTransferenceService
//    {
//        private readonly ITransferenceService _transferenceService;
//        private readonly IAccountService _accountService;
//        public UpdateTransferenceService(ITransferenceService transferenceService,
//                                        IAccountService accountService)
//        {
//            _transferenceService = transferenceService;
//        }

//        public async Task<bool> Processar(TransferRequestedEvent transference)
//        {
//            var transferenceUpdate = new Transference(transference.Id);

//            var apiResponseOriginUserAccount = await _accountService.GetAccountAndBalance(transference.AccountOrigin);

//            if (apiResponseOriginUserAccount.IsSuccessStatusCode)
//            {
//                var originUserAccount = apiResponseOriginUserAccount.Content;
//                if (originUserAccount.balance < transference.Amount)
//                {
//                    transferenceUpdate.UpdateStatus(TransferenceStatus.Error);
//                    transferenceUpdate.UpdateStatusDetail("Insufficient funds");
//                    return false;
//                }

//                var transferenceRequestOrigin = new TransferenceRequest(transference.AccountOrigin, transference.Amount, "Debit");
//                var transferenceRequestDestination = new TransferenceRequest(transference.AccountDestination, transference.Amount, "Credit");

//                //var resultsTransactions = await Task.WhenAll(
//                var a = await _accountService.AccountTrasaction(transferenceRequestOrigin);
//                var b = await _accountService.AccountTrasaction(transferenceRequestDestination);
//                //);

//                //if (resultsTransactions.Any(t => t.Equals(false)))
//                //{

//                //    transferenceUpdate.UpdateStatus(TransferenceStatus.Error);
//                //    transferenceUpdate.UpdateStatusDetail("Error when trying to transfer values");
//                //    //_transferenceService.Update(transferenceUpdate);
//                //    return;
//                //}

//                transferenceUpdate = new Transference(transference.Id);
//                transferenceUpdate.UpdateStatus(TransferenceStatus.Confirmed);
//                //_transferenceService.Update(transferenceUpdate);
//                return true;
//            }
//        }
//    }
//}
