﻿using Bank.Account.Service;
using Bank.Account.Service.Interfaces;
using Bank.Transaction.Application.Commands;
using Bank.Transaction.Application.Events;
using Bank.Transaction.Application.Services;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.Transfer.Domain.Services;
using Bank.Transfer.Infrastructure.Context;
using Bank.Transfer.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Bank.Transaction.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<BankContext>();
            services.AddScoped<ITransferenceRepository, TransferenceRepository>();
            services.AddScoped<ITransferenceService, TransferenceService>();
            services.AddScoped<ITransactionAppService, TransactionAppService>();

            services.AddRefitClient<IAccountService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ExternalAccountService:BaseUrl").Value));
            
            services.AddRefitClient<ITransferenceUpdateService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ExternalUpdateTransferenceService:BaseUrl").Value));

            //CQRS
            services.AddTransient<IMediatorHandler, MediatorHandler>();
            services.AddTransient<IRequestHandler<ProcessTransferenceCommand, bool>, TransferenceCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateTransferenceStatusCommand, bool>, TransferenceCommandHandler>();

            services.AddTransient<INotificationHandler<InsuficientBalanceEvent>, TransferenceEventHandler>();
            services.AddTransient<INotificationHandler<TransferenceNotFoundEvent>, TransferenceEventHandler>();

            return services;
        }
    }
}
