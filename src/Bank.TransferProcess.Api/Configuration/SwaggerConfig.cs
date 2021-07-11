using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Bank.TransferProcess.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Acesso Bank Backend Test C#",
                    Version = "v1",
                    Description = "Transfer Process API Swagger",
                    Contact = new OpenApiContact()
                    {
                        Email = "guilgerm@gmail.com",
                        Name = "Marcilia Guilger",
                        Url = new Uri("https://www.linkedin.com/in/marcilia-guilger-62661933/")
                    }
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
