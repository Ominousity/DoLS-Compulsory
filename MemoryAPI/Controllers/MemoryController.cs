﻿using Domain;
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
        [Route("GetCalculations")]
        public async Task<IActionResult> GetCalculations(Guid UserId)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Memory-API");
            using (var span = tracer.StartActiveSpan("GetCalculations"))
            {
                span.SetAttribute("User Id", UserId.ToString());
                try
                {
                    Log.Logger.Information($"Request for getting calculations from user : {UserId}");
                    List<Calculation> calculations = await _memoryRepository.GetCalculations(UserId);
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
        [Route("SaveCalculation")]
        public async Task<IActionResult> SaveCalculation(Calculation calculation)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Memory-API");
            using (var span = tracer.StartActiveSpan("SaveCalculation"))
            {
                span.SetAttribute("Calculation", calculation.ToString());
                try
                {
                    Log.Logger.Information($"Requst for saving a new calculation");
                    await _memoryRepository.SaveCalculation(calculation);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error while saving calculation");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost]
        [Route("Rebuild")]
        public async Task<IActionResult> Rebuild()
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Memory-API");
            using (var span = tracer.StartActiveSpan("Rebuild"))
            {
                try
                {
                    Log.Logger.Information($"Requst for rebuilding the database");
                    await _memoryRepository.RebuildAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error while rebuilding the database");
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
