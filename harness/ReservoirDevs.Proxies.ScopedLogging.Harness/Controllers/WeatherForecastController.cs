using Microsoft.AspNetCore.Mvc;
using ReservoirDevs.Proxies.ScopedLogging.Harness.Services.Interfaces;

namespace ReservoirDevs.Proxies.ScopedLogging.Harness.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ITestService _testService;

        public WeatherForecastController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_testService.IsTest("A", "B"));
        }
    }
}
