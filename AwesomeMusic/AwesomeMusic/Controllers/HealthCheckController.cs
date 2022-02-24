namespace AwesomeMusic.Controllers
{
    using System.Threading.Tasks;
    using AwesomeMusic.Data.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : Controller
    {
        private readonly IAwesomeMusicContext _context;
        private readonly string _healthCheckKey;

        public HealthCheckController(IConfiguration configuration, IAwesomeMusicContext context)
        {
            _healthCheckKey = configuration["HealthCheckKey"];
            _context = context;
        }

        [HttpGet("Check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckAsync()
        {
            var isOk =
                CheckKeyVaultConnection() &&
                await CheckDbConnectionAsync();

            return isOk
                ? Ok(isOk)
                : StatusCode(StatusCodes.Status500InternalServerError, isOk);
        }

        private bool CheckKeyVaultConnection()
            => !string.IsNullOrEmpty(_healthCheckKey) && bool.Parse(_healthCheckKey);

        private Task<bool> CheckDbConnectionAsync()
            => _context.CanConnectAsync();
    }
}
