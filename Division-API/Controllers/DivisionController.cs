using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Division_API.Controllers
{
    [ApiController]
    public class DivisionController : ControllerBase
    {
        [HttpGet]
        [Route("doDivision")]
        public IActionResult Division(float a, float b)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Division-API");
            using (var span = tracer.StartActiveSpan("Division"))
            {
                span.SetAttribute("Number A", a);
                span.SetAttribute("Number B", b);
                try
                {
                    Log.Logger.Information($"Request for division between : A = {a} & B = {b}");
                    return Ok(a / b);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error($"tried to divide A = {a} & B = {b}, division Failed!");
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
