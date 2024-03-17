using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Addition_API.Controllers
{
    [ApiController]
    public class AdditionController : ControllerBase
    {   
        [HttpGet]
        [Route("doAddition")]
        public async Task<IActionResult> Addition(float a, float b)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Addition-API");
            using (var span = tracer.StartActiveSpan("Addition"))
            {
                span.SetAttribute("Number A", a);
                span.SetAttribute("Number B", b);
                Log.Logger.Information($"Request for adding these 2 numbers : Number A = {a} Number B = {b}");
                return Ok(a + b);
            }
        }
    }
}
