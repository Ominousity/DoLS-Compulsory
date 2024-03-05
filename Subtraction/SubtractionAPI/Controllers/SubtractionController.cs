using Domain;
using Microsoft.AspNetCore.Mvc;

namespace SubtractionAPI;

[ApiController]
public class SubtractionController : ControllerBase
{
    private readonly ILogger<SubtractionController> _logger;
    public SubtractionController(ILogger<SubtractionController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    [ProducesResponseType(typeof(Calculation), 200)]
    [ProducesResponseType(400)]
    public IActionResult Subtraction(float a, float b)
    {
        try
        {
            _logger.LogInformation($"Request for subtracting these 2 numbers : Number A = {a} Number B = {b}");
            return Ok(a-b);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while subtracting numbers");
            return BadRequest(e.Message);
        }
    }
}
