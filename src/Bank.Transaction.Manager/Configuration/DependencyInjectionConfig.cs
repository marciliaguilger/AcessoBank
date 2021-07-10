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
using System;

namespace Bank.TransferProcess.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddDbContext<BankContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddRefitClient<IAccountService>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ExternalAccountService:BaseUrl").Value));

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<BankContext>();
            
            services.AddScoped<ITransferenceRepository, TransferenceRepository>();
            
            services.AddScoped<ITransferenceService, TransferenceService>();
            services.AddScoped<ITransferProcessService, TransferProcessService>();
            
            services.AddMediatR(typeof(Startup));

            services.AddTransient<IMediatorHandler, MediatorHandler>();
            services.AddTransient<IRequestHandler<TransferenceProcessCommand, bool>, TransferenceProcessCommandHandler>();
            services.AddTransient<IRequestHandler<TransferenceStatusUpdateCommand, bool>, TransferenceStatusUpdateCommandHandler>();

            return services;
        }
    }
}
