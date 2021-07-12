using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Dtos;
using Bank.Transfer.Domain.Core.Interface;
using Bank.Transfer.Domain.Enums;
using Bank.TransferProcess.Application.Commands;
using Bank.TransferProcess.Application.Dtos;
using Bank.TransferProcess.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bank.TransferProcess.Application.Service
{
    public class TransferProcessService : ITransferProcessService
    {
        private readonly IAccountService _accountService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ILogger<TransferProcessService> _logger;

        public TransferProcessService(IAccountService accountService,
                                        IMediatorHandler mediatorHandler,
                                        ILogger<TransferProcessService> logger)
        {
            _accountService = accountService;
            _mediatorHandler = mediatorHandler;
            _logger = logger;

        }

        public async Task<bool> Process(TransferenceProcessDto transferenceProcessDto)
        {
            await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Processing);
            var originAccount = await ValidateAccountAsync(transferenceProcessDto.Id, transferenceProcessDto.AccountOrigin);
            if (originAccount ==null)
            {
                await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Error, "Origin account not found");
                return false;
            }
            var destinationAccount = await ValidateAccountAsync(transferenceProcessDto.Id, transferenceProcessDto.AccountDestination);
            if (destinationAccount ==null)
            {
                await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Error, "Destination account not found");
                return false;
            }
            var validateFunder = await ValidateFunder(originAccount, transferenceProcessDto.Amount);
            if(!validateFunder)
            {
                await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Error, "Insuficient founds");
                return false;
            }

            var apiResponseAccountTransferenceDebit =  await AccountTransferenceDebit(transferenceProcessDto.AccountOrigin, transferenceProcessDto.Amount);
            if(!apiResponseAccountTransferenceDebit)
            {
                await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Error, "Error when trying to transfer amount");
                return false;
            }

            var apiResponseAccountTransferenceCredit = await AccountTransferenceCredit(transferenceProcessDto.AccountDestination, transferenceProcessDto.Amount);
            if(!apiResponseAccountTransferenceCredit)
            {
                await AccountTransferenceCredit(transferenceProcessDto.AccountOrigin, transferenceProcessDto.Amount);
                await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Error, "Error when trying to credit amount, reversed amount");
                return false;
            }
            
            await UpdateStatus(transferenceProcessDto.Id, TransferenceStatus.Confirmed);
            return true;

        }

        private async Task<bool> AccountTransferenceDebit(string account, decimal amount)
        {
            var transferenceRequestOriginDebit = new TransferenceRequest(account, amount, "Debit");
            var debitTransactionResult = await _accountService.AccountTrasaction(transferenceRequestOriginDebit);
            return debitTransactionResult.IsSuccessStatusCode;
        }
        private async Task<bool> AccountTransferenceCredit(string account, decimal amount)
        {
            var transferenceRequestOriginCredit = new TransferenceRequest(account, amount, "Credit");
            var debitTransactionResult = await _accountService.AccountTrasaction(transferenceRequestOriginCredit);
            return debitTransactionResult.IsSuccessStatusCode;
        }
       
        private async Task<UserAccount> ValidateAccountAsync(Guid id, string account)
        {
            var apiResponseUserAccount = await _accountService.GetAccountAndBalance(account);
            if (!apiResponseUserAccount.IsSuccessStatusCode)
            {
                return null;
            }
            return apiResponseUserAccount.Content;
        }
        private async Task<bool> ValidateFunder(UserAccount userAccount, decimal amount)
        {
            if (userAccount.balance < amount) return false;
            return true;
        }
        private async Task<bool> UpdateStatus(Guid transferenceId, TransferenceStatus status, string statusDetail ="")
        {
            _logger.LogInformation($"Transference id: {transferenceId} new status: {status} {statusDetail} ");
            var transferenceStatusUpdateCommand = new TransferenceStatusUpdateCommand(transferenceId, status, statusDetail);
            return await _mediatorHandler.SendCommand<TransferenceStatusUpdateCommand, bool>(transferenceStatusUpdateCommand);
        }
    }
}
