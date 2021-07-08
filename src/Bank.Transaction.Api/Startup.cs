using AutoMapper;
using Bank.Messaging.Receive.Options;
using Bank.Messaging.Receive.Receiver;
using Bank.Transaction.Api.BackgroundServices;
using Bank.Transaction.Api.Configuration;
using Bank.Transaction.Application.Services;
using Bank.Transfer.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Bank.Transaction.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BankContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));


            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMQConfigurations");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ITransactionAppService).Assembly);

            services.AddAutoMapper(typeof(Startup));

            //services.AddMediatR(typeof(Startup));
            services.AddControllers();

            //var rabbitMQConfigurations = new RabbitMQConfigurations();
            //new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
            //    Configuration.GetSection("RabbitMQConfigurations"))
            //        .Configure(rabbitMQConfigurations);
            //services.AddSingleton(rabbitMQConfigurations);

            services.ResolveDependencies(Configuration);
            services.AddHostedService<TransferenceRequestReceiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
