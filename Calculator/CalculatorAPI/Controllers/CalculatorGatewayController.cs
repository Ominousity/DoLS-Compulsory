﻿using Microsoft.AspNetCore.Mvc;
using Domain;
using CalcApplication;
using Serilog;

namespace CalculatorAPI;

[ApiController]
public class CalculatorGateway : ControllerBase
{
    private readonly ICalculationService calcService;

    public CalculatorGateway(ICalculationService _calcService)
    {
        calcService = _calcService;
    }

    [HttpPost]
    [Route("api/doCalculation")]
    [ProducesResponseType(typeof(Calculation), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Calculation>> GetAsync(Calculation calc)
    {
        var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Calculator-API");
        using (var span = tracer.StartActiveSpan("DoCalculation"))
        {
            span.SetAttribute("Operation", calc.Operation.ToString());
            try
            {
                Log.Logger.Information($"Calculation requested {calc.Operation}");
                return Ok(await calcService.DoCalculationAsync(calc));
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return BadRequest(e.Message);
            }
        }
    }

    [HttpGet]
    [Route("api/getCalculations")]
    [ProducesResponseType(typeof(List<Calculation>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<Calculation>>> GetCalculations(Guid id)
    {
        var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("Calculator-API");
        using (var span = tracer.StartActiveSpan("GetCalculations"))
        {
            span.SetAttribute("ID", id.ToString());
            try
            {
                Log.Logger.Information($"Calculations requested for {id}");
                return Ok(await calcService.GetCalculations(id));
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                return BadRequest(e.Message);
            }
        }
    }

}
