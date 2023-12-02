using DotNet8WebApiAuthentication.Authentication;
using DotNet8WebApiAuthentication.Authentication.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApiAuthentication.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IdentityUserAccessor accessor;
        private readonly UserManager<ApplicationUser> userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IdentityUserAccessor accessor, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.accessor = accessor;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var user = await accessor.GetRequiredUserAsync(HttpContext);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
