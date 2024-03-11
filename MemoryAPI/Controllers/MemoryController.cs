using Domain;
using MemoryRepository;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MemoryAPI.Controllers
{
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryRepository _memoryRepository;
        public MemoryController(IMemoryRepository memoryRepository)
        {
            _memoryRepository = memoryRepository;
        }

        [HttpGet]
        public IActionResult GetCalculations(Guid UserId)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Memory-API");
            using (var span = tracer.StartActiveSpan("GetCalculations"))
            {
                span.SetAttribute("User Id", UserId.ToString());
                try
                {
                    Log.Logger.Information($"Request for getting calculations from user : {UserId}");
                    List<Calculation> calculations = _memoryRepository.GetCalculations(UserId);
                    return Ok(calculations);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error while getting calculations");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult SaveCalculation(Calculation calculation)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Memory-API");
            using (var span = tracer.StartActiveSpan("SaveCalculation"))
            {
                span.SetAttribute("Calculation", calculation.ToString());
                try
                {
                    Log.Logger.Information($"Requst for saving a new calculation");
                    _memoryRepository.SaveCalculation(calculation);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error while saving calculation");
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
