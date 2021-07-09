using Bank.Account.Service.Dtos;
using Bank.Account.Service.Interfaces;
using Bank.Transaction.Application.Events;
using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Enums;
using Bank.Transfer.Domain.Interfaces.Service;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Transaction.Application.Commands
{
    public class TransferenceCommandHandler: 
        IRequestHandler<UpdateTransferenceStatusCommand, bool>,
        IRequestHandler<ProcessTransferenceCommand, bool>
    {
        private readonly ITransferenceService _transferenceService;
        private readonly IAccountService _accountService;
        private readonly ITransferenceUpdateService _transferenceUpdateService;
        public TransferenceCommandHandler(ITransferenceService transferenceService, 
                                            IAccountService accountService,
                                            ITransferenceUpdateService transferenceUpdateService)
        {
            _transferenceService = transferenceService;
            _accountService = accountService;
            _transferenceUpdateService = transferenceUpdateService;
        }
        public async Task<bool> Handle(UpdateTransferenceStatusCommand message, CancellationToken cancellationToken)
        {
            var transference = new Transference(message.Id);
            transference.UpdateStatus(message.TransferenceStatus);

            var exists = _transferenceService.GetById(message.Id);
            if (exists == null)
            {
                //transference.AddEvent(new TransferenceNotFoundEvent(message.Id, TransferenceStatus.Error, "Transference not found"));
                return false; 
            }

            _transferenceService.Update(transference);
            return await _transferenceService.Commit();
        }
        
        public async Task<bool> Handle(ProcessTransferenceCommand message, CancellationToken cancellationToken)
        {
            //var originUserAccount= await _accountService.GetAccountAndBalance(message.AccountOrigin);
            try
            {

                var apiResponseOriginUserAccount = await _accountService.GetAccountAndBalance(message.AccountOrigin);


                if (!apiResponseOriginUserAccount.IsSuccessStatusCode)
                {
                    var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, TransferenceStatus.Error, "Error when trying to get account data");
                    var apiResponse = _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
                    
                    //transferenceUpdate.UpdateStatus(TransferenceStatus.Error);
                    //transferenceUpdate.UpdateStatusDetail("Error when trying to get account data");
                    //await _transferenceService.UpdateAsync(transferenceUpdate);
                    //await _transferenceService.Commit();
                    return false;
                }
                var originUserAccount = apiResponseOriginUserAccount.Content;
                if (originUserAccount.balance < message.Amount)
                {
                    var transferenceUpdate1 = new TransferenceToUpdateRequest(message.Id, TransferenceStatus.Error, "Insufficient funds");
                    var apiResponse1 = _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate1);

                    //transferenceUpdate.UpdateStatus(TransferenceStatus.Error);
                    //transferenceUpdate.UpdateStatusDetail("Insufficient funds");
                    //await _transferenceService.UpdateAsync(transferenceUpdate);
                    //await _transferenceService.Commit();
                    return false;
                }

                var transferenceRequestOrigin = new TransferenceRequest(message.AccountOrigin, message.Amount, "Debit");
                var transferenceRequestDestination = new TransferenceRequest(message.AccountDestination, message.Amount, "Credit");

                var resultsTransactions = await Task.WhenAll(
                    _accountService.AccountTrasaction(transferenceRequestOrigin),
                    _accountService.AccountTrasaction(transferenceRequestDestination)
                );

                if (resultsTransactions.Any(t => t.Equals(false)))
                {
                    var transferenceUpdate2 = new TransferenceToUpdateRequest(message.Id, TransferenceStatus.Error, "Error when trying to transfer values");
                    var apiResponse2 = _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate2);

                    //transferenceUpdate.UpdateStatus(TransferenceStatus.Error);
                    //transferenceUpdate.UpdateStatusDetail("Error when trying to transfer values");
                    //await _transferenceService.UpdateAsync(transferenceUpdate);
                    //await _transferenceService.Commit();
                    return false;
                }

                var transferenceUpdate3 = new TransferenceToUpdateRequest(message.Id, TransferenceStatus.Confirmed);
                var apiResponse3 = _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate3);

                //transferenceUpdate = new Transference(message.Id);
                //transferenceUpdate.UpdateStatus(TransferenceStatus.Confirmed);
                //await _transferenceService.UpdateAsync(transferenceUpdate);
                //await _transferenceService.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //return false;
            }



            //if (originUserAccount.balance < message.Amount)
            //    originUserAccount.AddEvent(new InsuficientBalanceEvent(message.Id , ETransferenceStatus.Error, "Insuficient balance"));

            //var resultsTransactions = await Task.WhenAll(
            //    _accountService.AccountDebit(message.AccountOrigin, message.Amount),
            //    _accountService.AccountCredit(message.AccountDestination, message.Amount)
            //);

            //if(resultsTransactions.Any(t => t.Equals(false)))
            //{
            //    //rollback?
            //    return false;
            //}
            return true;
        }
    }
}
