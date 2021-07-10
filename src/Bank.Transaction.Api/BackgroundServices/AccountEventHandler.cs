//using Bank.Transaction.Application.Commands;
//using Bank.Transaction.Application.Services;
//using Bank.Transfer.Application.Events;
//using Bank.Transfer.Domain.Core.Communication;
//using Bank.Transfer.Domain.Enums;
//using EasyNetQ;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Bank.Transaction.Api.BackgroundServices
//{
//    public class AccountEventHandler 
//    {
//        //private readonly IConfiguration _configuration;
//        //private IBus _bus;
//        //private readonly IMediatorHandler _mediatorHandler;
//        //private readonly string _rabbitConnectionString;
//        private readonly ITransactionAppService _transactionAppService;

//        public AccountEventHandler(ITransactionAppService transactionAppService)
//        {
//            _transactionAppService = transactionAppService;
//            //_configuration = configuration;
//            //_mediatorHandler = mediatorHandler;

//            //_rabbitConnectionString = _configuration
//            //    .GetSection("RabbitMQConfigurations")
//            //    .GetSection("Connection").Value;
//        }
//        public async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
            
//            //_bus = RabbitHutch.CreateBus(_rabbitConnectionString);
//            //_bus.PubSub.Subscribe<TransferRequestedEvent>("account-transaction", ProccessTransference);

//            //while (!stoppingToken.IsCancellationRequested)
//            //{
//            //    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
//            //}
//            //_bus.Dispose();
//        }

//        //private void ProccessTransference(TransferRequestedEvent transference)
//        //{
//        //    var command = new ProcessTransferenceCommand(transference.Id,
//        //                                                    transference.AccountOrigin,
//        //                                                    transference.AccountDestination,
//        //                                                    transference.Amount);
//        //    var result = _mediatorHandler.SendCommand(command).Result;
//        //    if(!result)
//        //    {
//        //        _mediatorHandler.SendCommand(new UpdateTransferenceStatusCommand(transference.Id, ETransferenceStatus.Error));
//        //        return;
//        //    }
//        //    _mediatorHandler.SendCommand(new UpdateTransferenceStatusCommand(transference.Id, ETransferenceStatus.Confirmed));
//        //}
//    }
//}
