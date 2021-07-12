using AutoMapper;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.Transfer.Domain.Options;
using Bank.Transfer.Domain.Services;
using Bank.Transfer.Infrastructure.Context;
using Bank.Transfer.Infrastructure.Repository;
using Bank.TransferRequest.Application.Commands;
using Bank.TransferRequest.Application.Dtos;
using Bank.TransferRequest.Application.Events;
using Bank.TransferRequest.Application.Interfaces;
using Bank.TransferRequest.Application.Queries;
using Bank.TransferRequest.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Bank.TransferRequest.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<BankContext>();
            services.AddScoped<ITransferenceRepository, TransferenceRepository>();
            services.AddScoped<ITransferenceService, TransferenceService>();
            services.AddScoped<ITransferenceAppService, TransferenceAppService>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<TransferAmountCommand, TransferAmountDto>, TransferAmountCommandHandler>();
            services.AddScoped<IRequestHandler<RequestStatusQuery, RequestStatusDto>, RequestStatusQueryHandler>();
            services.AddScoped<INotificationHandler<TransferRequestedEvent>, TransferenceEventHandler>();

            services.AddDbContext<BankContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            
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
