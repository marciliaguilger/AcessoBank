using Bank.Transfer.Application.Dtos;
using Bank.Transfer.Domain.Enums;
using Bank.TransferProcess.Application.Dtos;
using Bank.TransferProcess.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Application.Commands
{
    public class TransferenceProcessCommandHandler :
        IRequestHandler<TransferenceProcessCommand, bool>
    {
        private readonly ITransferProcessService _transferProcessService;
        public TransferenceProcessCommandHandler(ITransferProcessService transferProcessService)
        {
            _transferProcessService = transferProcessService;
        }

        public async Task<bool> Handle(TransferenceProcessCommand message, CancellationToken cancellationToken)
        {
            var transferenceProcessDto = new TransferenceProcessDto(message.Id, message.AccountOrigin, message.AccountDestination, message.Amount);
            var processResult = await _transferProcessService.Process(transferenceProcessDto);
            return processResult;
            //TransferenceStatus transferenceStatus = TransferenceStatus.Processing;
            //string statusDetail;

            //var transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, transferenceStatus);
            //await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);

            //var apiResponseOriginUserAccount = await _accountService.GetAccountAndBalance(message.AccountOrigin);
            //if(!apiResponseOriginUserAccount.IsSuccessStatusCode)
            //{
            //    transferenceStatus = TransferenceStatus.Error;
            //    statusDetail = "Origin account  not found";
            //    transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, transferenceStatus, statusDetail);
            //    await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);
            //    return false;
            //}

            //var apiResponseDestinationUserAccount = await _accountService.GetAccountAndBalance(message.AccountDestination);
            //if(!apiResponseOriginUserAccount.IsSuccessStatusCode)
            //{
            //    transferenceStatus = TransferenceStatus.Error;
            //    statusDetail = "Destination account  not found";
            //    transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, transferenceStatus, statusDetail);
            //    await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);
            //    return false;
            //}

            //var originUserAccount = apiResponseOriginUserAccount.Content;
            //if (originUserAccount.balance < message.Amount)
            //{
            //    transferenceStatus = TransferenceStatus.Error;
            //    statusDetail = "Insufficient funds";
            //    transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, transferenceStatus, statusDetail);
            //    await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);
            //    return false;
            //}

            //var transferenceRequestOriginDebit = new TransferenceRequest(message.AccountOrigin, message.Amount, "Debit");
            //var transferenceRequestDestinationCredit = new TransferenceRequest(message.AccountDestination, message.Amount, "Credit");

            //var debitTransactionResult = _accountService.AccountTrasaction(transferenceRequestOriginDebit);
            //var creditTransactionResult = _accountService.AccountTrasaction(transferenceRequestDestinationCredit);

            ////var resultsTransactions = await Task.WhenAll(
            ////);

            ////TODO: FAZER UM ROLLBACK (TRANSAÇAO DE CREDITO) SE DER ERRO NO CRÉDITO
            ////if (resultsTransactions.Any(t => t.Equals(false)))
            ////{
            ////    transferenceStatus = TransferenceStatus.Error;
            ////    statusDetail = "Error when trying to transfer values";

            ////    transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, transferenceStatus, statusDetail);
            ////    await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);
            ////    return false;
            ////}

            //transferenceStatus = TransferenceStatus.Error;
            //statusDetail = "Error when trying to transfer values";

            //transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(message.Id, transferenceStatus, statusDetail);
            //await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);
            //return false;

            //transferenceStatus = TransferenceStatus.Confirmed;

            //var transferenceStatusUpdateCommandProcessed = new TransferenceStatusUpdateCommand(message.Id, TransferenceStatus.Confirmed);
            //await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);

            //var apiResponseDestinationUserAccount = await _accountService.GetAccountAndBalance(message.AccountDestination);


            //if (!apiResponseOriginUserAccount.IsSuccessStatusCode || !apiResponseDestinationUserAccount.IsSuccessStatusCode)
            //{
            //    //var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
            //    //var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
            //    return false;
            //}

            //var originUserAccount = apiResponseOriginUserAccount.Content;
            //if (originUserAccount.balance < message.Amount)
            //{
            //    statusDetail = "Insufficient funds";
            //    //var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
            //    //var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
            //    return false;
            //}

            //var transferenceRequestOrigin = new TransferenceRequest(message.AccountOrigin, message.Amount, "Debit");
            //var transferenceRequestDestination = new TransferenceRequest(message.AccountDestination, message.Amount, "Credit");

            //var resultsTransactions = await Task.WhenAll(
            //    _accountService.AccountTrasaction(transferenceRequestOrigin),
            //    _accountService.AccountTrasaction(transferenceRequestDestination)
            //);

            //if (resultsTransactions.Any(t => t.Equals(false)))
            //{
            //    statusDetail = "Error when trying to transfer values";

            //    //var transferenceUpdate = new TransferenceToUpdateRequest(message.Id, transferenceStatus, statusDetail);
            //    //var apiResponse = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdate);
            //    return false;
            //}

            //transferenceStatus = TransferenceStatus.Confirmed;

            //var transferenceUpdateSucces = new TransferenceToUpdateRequest(message.Id, transferenceStatus);
            //var apiResponseSucces = await _transferenceUpdateService.UpdateTansferenceStatus(transferenceUpdateSucces);
            //return apiResponseSucces.IsSuccessStatusCode;
        }
    }
}
