using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Dtos;
using Bank.Transfer.Domain.Core.Interface;
using Bank.TransferProcess.Application.Dtos;
using Bank.TransferProcess.Application.Service;
using Moq;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bank.TransferProcess.Application.Tests.Services
{
    public class TransferProcessServiceTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;


        public TransferProcessServiceTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
        }

        [Fact(DisplayName = "Transfer process - success")]
        [Trait("Category", "Transfer Request - Command Handlers")]
        public async Task TransferProcessService_Process_ShouldReturnTrue()
        {
            var httpResponseMessage = new HttpResponseMessage();
            var amount = 10.0M;
            Guid id = Guid.NewGuid();

            var originaccountNumber = "89476598";
            var originUserAccount = new UserAccount();
            originUserAccount.id = 1;
            originUserAccount.accountNumber = originaccountNumber;
            originUserAccount.balance = 100;

            var responseApiOriginUserAccount = new ApiResponse<UserAccount>(httpResponseMessage, originUserAccount);

            var destinationAccountNumber = "86744746";
            var destinationUserAccount = new UserAccount();
            destinationUserAccount.id = 2;
            destinationUserAccount.accountNumber = destinationAccountNumber;
            destinationUserAccount.balance = 100;

            var responseApiDestinationUserAccount = new ApiResponse<UserAccount>(httpResponseMessage, destinationUserAccount);

            _accountServiceMock.Setup(s => s.GetAccountAndBalance(originaccountNumber)).Returns(Task.FromResult(responseApiOriginUserAccount));
            _accountServiceMock.Setup(s => s.GetAccountAndBalance(destinationAccountNumber)).Returns(Task.FromResult(responseApiDestinationUserAccount));
            _accountServiceMock.Setup(s => s.AccountTrasaction(It.IsAny<TransferenceRequest>())).Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            var transferenceProcessDto = new TransferenceProcessDto(id,originaccountNumber,destinationAccountNumber,amount);
            
            var transferProcessService = new TransferProcessService(_accountServiceMock.Object, _mediatorHandlerMock.Object);
            var result = await transferProcessService.Process(transferenceProcessDto);

            Assert.True(result);
        }

        [Fact(DisplayName = "Transfer process - not success")]
        [Trait("Category", "Transfer Request - Command Handlers")]
        public async Task TransferProcessService_Process_ShouldReturnFalse()
        {
            var httpResponseMessage = new HttpResponseMessage();
            var amount = 10.0M;
            Guid id = Guid.NewGuid();

            var originaccountNumber = "89476598";
            var originUserAccount = new UserAccount();
            originUserAccount.id = 1;
            originUserAccount.accountNumber = originaccountNumber;
            originUserAccount.balance = 100;

            var responseApiOriginUserAccount = new ApiResponse<UserAccount>(httpResponseMessage, originUserAccount);

            var destinationAccountNumber = "86744746";
            var destinationUserAccount = new UserAccount();
            destinationUserAccount.id = 2;
            destinationUserAccount.accountNumber = destinationAccountNumber;
            destinationUserAccount.balance = 100;

            var responseApiDestinationUserAccount = new ApiResponse<UserAccount>(httpResponseMessage, destinationUserAccount);

            _accountServiceMock.Setup(s => s.GetAccountAndBalance(originaccountNumber)).Returns(Task.FromResult(responseApiOriginUserAccount));
            _accountServiceMock.Setup(s => s.GetAccountAndBalance(destinationAccountNumber)).Returns(Task.FromResult(responseApiDestinationUserAccount));
            _accountServiceMock.Setup(s => s.AccountTrasaction(It.IsAny<TransferenceRequest>())).Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            var transferenceProcessDto = new TransferenceProcessDto(id, originaccountNumber, destinationAccountNumber, amount);

            var transferProcessService = new TransferProcessService(_accountServiceMock.Object, _mediatorHandlerMock.Object);
            var result = await transferProcessService.Process(transferenceProcessDto);

            Assert.True(!result);
        }
    }
}
