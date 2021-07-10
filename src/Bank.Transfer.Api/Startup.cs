using AutoMapper;
using Bank.Transfer.Api.Configuration;
using Bank.Transfer.Application.Options;
using Bank.Transfer.Infrastructure.Context;
using Bank.TransferRequest.Api.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Bank.Transfer.Api
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
            services.AddControllers();
            services.AddSwaggerConfiguration();
            services.ResolveDependencies(Configuration);

            //services.AddDbContext<BankContext>(options =>
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));
            
            //services.AddAutoMapper(typeof(Startup));

            //services.AddMediatR(typeof(Startup));


            //var serviceClientSettingsConfig = Configuration.GetSection("RabbitMQConfigurations");
            //services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerSetup();
        }
    }
}
