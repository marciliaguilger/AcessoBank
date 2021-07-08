using Bank.Transfer.Application.Commands;
using Bank.Transfer.Application.Events;
using Bank.Transfer.Application.Interfaces;
using Bank.Transfer.Application.Services;
using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.Transfer.Domain.Services;
using Bank.Transfer.Infrastructure.Context;
using Bank.Transfer.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Transfer.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BankContext>();
            services.AddScoped<ITransferenceRepository, TransferenceRepository>();
            services.AddScoped<ITransferenceService, TransferenceService>();
            services.AddScoped<ITransferenceAppService, TransferenceAppService>();
            //SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Bank Transference",
                    Version = "V1"
                });
            });


            //CQRS
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<TransferAmountCommand, bool>, TransferenceCommandHandler>();
            services.AddScoped<INotificationHandler<TransferRequestedEvent>, TransferenceEventHandler>();
            

            
            return services;
        }
    }
}
