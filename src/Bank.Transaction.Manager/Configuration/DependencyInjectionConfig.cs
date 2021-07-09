using Bank.Transfer.Domain.Core.Communication;
using Bank.Transfer.Domain.Interfaces.Repositories;
using Bank.Transfer.Domain.Interfaces.Service;
using Bank.Transfer.Domain.Services;
using Bank.Transfer.Infrastructure.Context;
using Bank.Transfer.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Transaction.Update.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddDbContext<BankContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<BankContext>();
            services.AddScoped<ITransferenceRepository, TransferenceRepository>();
            services.AddScoped<ITransferenceService, TransferenceService>();

            return services;
        }

    }
}
