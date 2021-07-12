using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Interface;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.Transfer.Domain.Services;
using Bank.Transfer.Infrastructure.Context;
using Bank.Transfer.Infrastructure.Repository;
using Bank.TransferProcess.Application.Commands;
using Bank.TransferProcess.Application.Interfaces;
using Bank.TransferProcess.Application.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Bank.TransferProcess.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddDbContext<BankContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<BankContext>();

            services.AddRefitClient<IAccountService>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ExternalAccountService:BaseUrl").Value));

            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<TransferenceProcessCommand, bool>, TransferenceProcessCommandHandler>();
            services.AddScoped<IRequestHandler<TransferenceStatusUpdateCommand, bool>, TransferenceStatusUpdateCommandHandler>();

            services.AddScoped<ITransferProcessService, TransferProcessService>();
            services.AddScoped<ITransferenceService, TransferenceService>();
            services.AddScoped<ITransferenceRepository, TransferenceRepository>();

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
