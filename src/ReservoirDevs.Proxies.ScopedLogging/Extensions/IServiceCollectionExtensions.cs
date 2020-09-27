using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ReservoirDevs.Proxies.ScopedLogging.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static void AddTransientProxy<TInterface, TImplementation>(this IServiceCollection serviceCollection) where TInterface : class where TImplementation : class, TInterface
        {
            serviceCollection.AddTransient<TInterface, TImplementation>();

            var provider = serviceCollection.BuildServiceProvider();
            var implementation = provider.GetService<TInterface>();
            var logger = provider.GetService<ILogger<TImplementation>>();

            var proxy = ScopedLoggingInterceptor<TInterface>.Create(implementation, logger);

            var descriptor = new ServiceDescriptor(typeof(TInterface), proxy);

            serviceCollection.Replace(descriptor);
        }
    }
}