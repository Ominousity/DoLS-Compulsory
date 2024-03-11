using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MutiplecationAPI.Controllers
{
    [ApiController]
    public class MutiplecationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Mutiply(float a, float b)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Multiplication-API");
            using (var span = tracer.StartActiveSpan("Multiplication"))
            {
                span.SetAttribute("Number A", a);
                span.SetAttribute("Number B", b);
                Log.Logger.Information($"Request for multiplying these 2 numbers : Number A = {a} Number B = {b}");
                return Ok(a * b);
            }
        }
    }
}
