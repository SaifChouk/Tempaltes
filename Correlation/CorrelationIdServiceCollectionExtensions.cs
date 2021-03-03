using Microsoft.Extensions.DependencyInjection;
using System;

namespace Correlation
{
    public static class CorrelationIdServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationId(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICorrelationAccessor, CorrelationAccessor>();

            return serviceCollection;
        }

        public static IServiceCollection AddCorrelationId(this IServiceCollection serviceCollection, Action<CorrelationConfiguration> options)
        {
            serviceCollection.AddCorrelationId();
            serviceCollection.Configure(options);

            return serviceCollection;
        }
    }
}
