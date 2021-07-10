//using Bank.Account.Service.Dtos;
//using Bank.Account.Service.Interfaces;
//using Bank.Transaction.Application.Events;
//using Bank.Transfer.Domain.Entities;
//using Bank.Transfer.Domain.Enums;
//using Bank.Transfer.Domain.Interfaces.Service;
//using MediatR;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Bank.Transaction.Application.Commands
//{
//    public class TransferenceCommandHandler :
//        IRequestHandler<UpdateTransferenceStatusCommand, bool>,
//        IRequestHandler<ProcessTransferenceCommand, bool>
//    {
//        private readonly IAccountService _accountService;
//        private readonly ITransferenceUpdateService _transferenceUpdateService;
//        public TransferenceCommandHandler(IAccountService accountService,
//                                        ITransferenceUpdateService transferenceUpdateService)
//        {
//            _accountService = accountService;
//            _transferenceUpdateService = transferenceUpdateService;
//        }
//        public async Task<bool> Handle(UpdateTransferenceStatusCommand message, CancellationToken cancellationToken)
//        {
//            var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, message.TransferenceStatus);
//            var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
//            return apiResponse.IsSuccessStatusCode;                        
//        }

//        public async Task<bool> Handle(ProcessTransferenceCommand message, CancellationToken cancellationToken)
//        {
//            try
//            {
//                TransferenceStatus transferenceStatus = TransferenceStatus.Error;
//                string statusDetail;

//                var apiResponseOriginUserAccount = await _accountService.GetAccountAndBalance(message.AccountOrigin);
//                var apiResponseDestinationUserAccount = await _accountService.GetAccountAndBalance(message.AccountDestination);


//                if (!apiResponseOriginUserAccount.IsSuccessStatusCode || !apiResponseDestinationUserAccount.IsSuccessStatusCode)
//                {
//                    statusDetail = "Account not found";
//                    var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
//                    var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
//                    return false;
//                }

//                var originUserAccount = apiResponseOriginUserAccount.Content;
//                if (originUserAccount.balance < message.Amount)
//                {
//                    statusDetail = "Insufficient funds";
//                    var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
//                    var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
//                    return false;
//                }

//                var transferenceRequestOrigin = new TransferenceRequest(message.AccountOrigin, message.Amount, "Debit");
//                var transferenceRequestDestination = new TransferenceRequest(message.AccountDestination, message.Amount, "Credit");

//                var resultsTransactions = await Task.WhenAll(
//                    _accountService.AccountTrasaction(transferenceRequestOrigin),
//                    _accountService.AccountTrasaction(transferenceRequestDestination)
//                );

//                if (resultsTransactions.Any(t => t.Equals(false)))
//                {
//                    statusDetail = "Error when trying to transfer values";

//                    var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
//                    var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
//                    return false;
//                }

//                transferenceStatus = TransferenceStatus.Confirmed;

//                var transferenceUpdateSucces = new TransferenceToUpdateRequest(message.Id, transferenceStatus);
//                var apiResponseSucces = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdateSucces);
//                return apiResponseSucces.IsSuccessStatusCode;


//            }
//            catch (Exception ex)
//            {
//                //gerar log
//                return false;
//            }
//        }
//    }
//}
