using Microsoft.AspNetCore.Mvc;

namespace Addition_API.Controllers
{
    [ApiController]
    public class AdditionController : ControllerBase
    {   
        private readonly ILogger<AdditionController> _logger;
        public AdditionController(ILogger<AdditionController> logger) { 
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Addition(float a, float b)
        {
            _logger.LogInformation($"request for addition for : A = {a} & B = {b}");
            return Ok(a + b);
        }
    }
}
