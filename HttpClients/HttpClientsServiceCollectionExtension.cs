using HttpClients.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpClients
{
    public static class HttpClientsServiceCollectionExtension
    {

        public static IServiceCollection AddHttpsClients(this IServiceCollection services, IConfiguration configuration)
        {

          
            services.AddTransient<DefaultLoggingHttpMessageHandler>();
            services.AddTransient<CorrelationIdHttpMessageHandler>();
            return services;
        }
    }
}
