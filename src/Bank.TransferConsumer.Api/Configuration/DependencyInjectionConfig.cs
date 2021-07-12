using Bank.Transfer.Domain.Core.Interface;
using Bank.Transfer.Domain.Options;
using Bank.TransferConsumer.Application.Interfaces;
using Bank.TransferConsumer.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Bank.TransferConsumer.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionAppService, TransferenceConsumerAppService>();
            services.AddRefitClient<ITransferenceProcessService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("TransferenceProcessService:BaseUrl").Value));
            
            var serviceClientSettingsConfig = configuration.GetSection("RabbitMQConfigurations");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            var elasticUri = configuration.GetSection("ElasticConfiguration:Uri");
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri.Value))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            })
            .CreateLogger();

            return services;
        }
    }
}
