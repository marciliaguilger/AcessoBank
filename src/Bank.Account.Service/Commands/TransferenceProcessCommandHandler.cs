using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Dtos;
using Bank.Transfer.Domain.Core.Interface;
using Bank.Transfer.Domain.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Application.Commands
{
    public class TransferenceProcessCommandHandler :
        IRequestHandler<TransferenceProcessCommand, bool>
    {
        private readonly IAccountService _accountService;
        //private readonly ITransferenceUpdateService _transferenceUpdateService;
        private readonly IMediatorHandler _mediatorHandler;
        public TransferenceProcessCommandHandler(IAccountService accountService,
                                        //ITransferenceUpdateService transferenceUpdateService,
                                        IMediatorHandler mediatorHandler)
        {
            _accountService = accountService;
            //_transferenceUpdateService = transferenceUpdateService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(TransferenceProcessCommand message, CancellationToken cancellationToken)
        {
            try
            {
                var transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, TransferenceStatus.Processing);
                await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);

                
                var mars = "guilger";
                

                var transferenceStatusUpdateCommandProcessed = new TransferenceStatusUpdateCommand(message.Id, TransferenceStatus.Confirmed);
                await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);



                TransferenceStatus transferenceStatus = TransferenceStatus.Error;
                string statusDetail;

                var apiResponseOriginUserAccount = await _accountService.GetAccountAndBalance(message.AccountOrigin);
                var apiResponseDestinationUserAccount = await _accountService.GetAccountAndBalance(message.AccountDestination);


                if (!apiResponseOriginUserAccount.IsSuccessStatusCode || !apiResponseDestinationUserAccount.IsSuccessStatusCode)
                {
                    statusDetail = "Account not found";
                    //var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
                    //var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
                    return false;
                }

                var originUserAccount = apiResponseOriginUserAccount.Content;
                if (originUserAccount.balance < message.Amount)
                {
                    statusDetail = "Insufficient funds";
                    //var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
                    //var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
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
                    statusDetail = "Error when trying to transfer values";

                    //var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
                    //var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
                    return false;
                }

                transferenceStatus = TransferenceStatus.Confirmed;

                //var transferenceUpdateSucces = new TransferenceToUpdateRequest(message.Id, transferenceStatus);
                //var apiResponseSucces = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdateSucces);
                //return apiResponseSucces.IsSuccessStatusCode;
                return true;

            }
            catch (Exception ex)
            {
                //gerar log
                return false;
            }
        }
    }
}
