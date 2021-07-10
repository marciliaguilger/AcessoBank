using AutoMapper;
using Bank.Transfer.Application.Commands;
using Bank.Transfer.Application.Events;
using Bank.Transfer.Application.Interfaces;
using Bank.Transfer.Application.Options;
using Bank.Transfer.Application.Queries;
using Bank.Transfer.Application.Services;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Core.Events;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.Transfer.Domain.Services;
using Bank.Transfer.Infrastructure.Context;
using Bank.Transfer.Infrastructure.Repository;
using Bank.TransferRequest.Application.Dtos;
using Bank.TransferRequest.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Transfer.Api.Configuration
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
            services.AddScoped<IRequestHandler<TransferAmountCommand, TransferAmountDto>, TransferenceCommandHandler>();
            services.AddScoped<IRequestHandler<RequestStatusQuery, RequestStatusDto>, RequestStatusQueryHandler>();
            services.AddScoped<INotificationHandler<TransferRequestedEvent>, TransferenceEventHandler>();

            services.AddDbContext<BankContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            
            var serviceClientSettingsConfig = configuration.GetSection("RabbitMQConfigurations");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            return services;
        }
    }
}
