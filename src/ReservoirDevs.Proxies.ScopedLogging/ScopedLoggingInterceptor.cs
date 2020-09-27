using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace ReservoirDevs.Proxies.ScopedLogging
{
    public class ScopedLoggingInterceptor<T> : DispatchProxy
    {
        private T _decorated;
        private ILogger<T> _logger;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var scope = new Dictionary<string, string> { { "class", typeof(T).FullName }, { "method", targetMethod.Name } };

            using (_logger.BeginScope(scope))
            {
                _logger.LogInformation("Calling {methodName}", targetMethod.Name);
                return targetMethod.Invoke(_decorated, args);
            }
        }

        public static T Create(T decorated, ILogger<T> logger)
        {
            object proxy = Create<T, ScopedLoggingInterceptor<T>>();
            ((ScopedLoggingInterceptor<T>)proxy).InjectServices(decorated, logger);
            return (T)proxy;
        }

        private void InjectServices(T decorated, ILogger<T> logger)
        {
            _decorated = decorated;
            _logger = logger;
        }
    }
}
