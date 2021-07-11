using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.TransferRequest.Application.Commands;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bank.TransferRequest.Application.Tests.Commands
{
    public class TransferAmountCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly TransferAmountCommandHandler _transferAmountCommandHandler;

        public TransferAmountCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _transferAmountCommandHandler = _mocker.CreateInstance<TransferAmountCommandHandler>();
        }

        [Fact(DisplayName = "Transfer amount - success")]
        [Trait("Category", "Transfer Request - Command Handlers")]
        public async Task TransferAmountCommandHandler_TransferRequest_ShouldSuccess()
        {
            var transferAmountCommand = new TransferAmountCommand("89476598", "86744746", 10.0M);
            
            var result = await _transferAmountCommandHandler.Handle(transferAmountCommand, CancellationToken.None);
            
            Assert.True(result != null);
            _mocker.GetMock<ITransferenceService>().Verify(s => s.Add(It.IsAny<Transference>()), Times.Once);
            _mocker.GetMock<ITransferenceService>().Verify(s => s.Commit(), Times.Once);
            
        }
        
        [Fact(DisplayName = "Transfer amount - success")]
        [Trait("Category", "Transfer Request - Command Handlers")]
        public async Task TransferAmountCommandHandler_TransferRequest_ShouldNotBeSuccess()
        {
            var transferAmountCommand = new TransferAmountCommand("89476598", "86744746", -10.0M);
            
            var result = await _transferAmountCommandHandler.Handle(transferAmountCommand, CancellationToken.None);
            
            Assert.True(result == null);
            
        }

    }
}
