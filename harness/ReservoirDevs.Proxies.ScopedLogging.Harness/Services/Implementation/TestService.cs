using ReservoirDevs.Proxies.ScopedLogging.Harness.Services.Interfaces;

namespace ReservoirDevs.Proxies.ScopedLogging.Harness.Services.Implementation
{
    public class TestService : ITestService
    {
        public bool IsTest(string one, string two)
        {
            return true;
        }
    }
}