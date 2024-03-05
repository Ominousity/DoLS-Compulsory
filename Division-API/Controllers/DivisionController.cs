using Microsoft.AspNetCore.Mvc;

namespace Division_API.Controllers
{
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private readonly ILogger<DivisionController> _logger;
        public DivisionController(ILogger<DivisionController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Division(float a, float b)
        {
            try
            {
                _logger.LogInformation($"Request for division between : A = {a} & B = {b}");
                return Ok(a / b);
            } catch (Exception ex) 
            {
                _logger.LogError($"tried to divide A = {a} & B = {b}, division Failed!");
                return BadRequest(ex.Message); 
            }
        }
    }
}
