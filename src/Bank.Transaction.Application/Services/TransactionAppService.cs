using Bank.Account.Service.Dtos;
using Bank.Account.Service.Interfaces;
using Bank.Transaction.Application.Commands;
using Bank.Transfer.Application.Events;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Enums;
using Bank.Transfer.Domain.Interfaces.Service;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Transaction.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {

        //private readonly IConfiguration _configuration;
        //private IBus _bus;
        //private readonly string _rabbitConnectionString;
        private readonly IAccountService _accountService;
        private readonly ITransferenceService _transferenceService;
        private readonly IMediator _mediator;

        public TransactionAppService(
            //IConfiguration configuration,
                                    IAccountService accountService,
                                    ITransferenceService transferenceService,
                                    IMediator mediator)
        {
            //_configuration = configuration;
            _accountService = accountService;
            _transferenceService = transferenceService;
            _mediator = mediator;
            //_rabbitConnectionString = _configuration
            //    .GetSection("RabbitMQConfigurations")
            //    .GetSection("Connection").Value;

            //_bus = RabbitHutch.CreateBus(_rabbitConnectionString);
        }
        //public async Task ListenMessages()
        //{
        //    _bus.PubSub.Subscribe<TransferRequestedEvent>("account-transaction", ProccessTransferenceAsync);

        //    //while (!stoppingToken.IsCancellationRequested)
        //    //{
        //    //    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        //    //}
        //    //_bus.Dispose();
        //}

        public  async void ProccessTransferenceAsync(TransferRequestedEvent transference)
        {
            try
            {
                await Task.Run(() => _mediator.Send(new ProcessTransferenceCommand(
                                                        transference.Id,
                                                        transference.AccountDestination,
                                                        transference.AccountOrigin,
                                                        transference.Amount)));

                //var result = _mediator.Send(new ProcessTransferenceCommand(
                //                                        transference.Id,
                //                                        transference.AccountDestination,
                //                                        transference.AccountOrigin,
                //                                        transference.Amount));
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }


        }
    }
}
