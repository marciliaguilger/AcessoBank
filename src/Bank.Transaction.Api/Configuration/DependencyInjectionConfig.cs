using Bank.Transaction.Application.Interfaces;
using Bank.Transaction.Application.Services;
using Bank.Transfer.Domain.Core.Interface;
using Bank.Transfer.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Bank.TransferConsumer.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionAppService, TransactionAppService>();
            services.AddRefitClient<ITransferenceProcessService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("TransferenceProcessService:BaseUrl").Value));
            
            var serviceClientSettingsConfig = configuration.GetSection("RabbitMQConfigurations");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);
            
            return services;
        }
    }
}
