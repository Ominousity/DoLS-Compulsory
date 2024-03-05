using Microsoft.AspNetCore.Mvc;

namespace MutiplecationAPI.Controllers
{
    [ApiController]
    public class MutiplecationController : ControllerBase
    {
        private readonly ILogger<MutiplecationController> _logger;
        public MutiplecationController(ILogger<MutiplecationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Mutiply(float a, float b)
        {
            _logger.LogInformation($"Request for multiplying these 2 numbers : Number A = {a} Number B = {b}");
            return Ok(a * b);
        }
    }
}
