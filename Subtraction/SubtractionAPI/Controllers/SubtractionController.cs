using Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SubtractionAPI;

[ApiController]
public class SubtractionController : ControllerBase
{
    [HttpPost]
    [Route("doSubtraction")]
    [ProducesResponseType(typeof(Calculation), 200)]
    [ProducesResponseType(400)]
    public IActionResult Subtraction(float a, float b)
    {
        var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Subtraction-API");
        using (var span = tracer.StartActiveSpan("Subtraction"))
        {
            span.SetAttribute("Number A", a);
            span.SetAttribute("Number B", b);
            try
            {
                Log.Logger.Information($"Request for subtracting these 2 numbers : Number A = {a} Number B = {b}");
                return Ok(a - b);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Error while subtracting numbers");
                return BadRequest(e.Message);
            }
        }
    }
}
