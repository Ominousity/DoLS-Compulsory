using Microsoft.AspNetCore.Mvc;
using Domain;
using CalcApplication;

namespace CalculatorAPI;

[ApiController]
public class CalculatorGateway : ControllerBase
{
    private readonly ICalculationService calcService;
    private readonly ILogger<CalculatorGateway> logger;

    public CalculatorGateway(ICalculationService _calcService, ILogger<CalculatorGateway> logger)
    {
        calcService = _calcService;
        this.logger = logger;
    }

    [HttpPost]
    [Route("api/doCalculation")]
    [ProducesResponseType(typeof(Calculation), 200)]
    [ProducesResponseType(400)]
    public ActionResult<Calculation> Get(Calculation calc)
    {
        try
        {
            logger.LogInformation($"Calculation requested {calc.Operation.ToString()}");
            return Ok(calcService.DoCalculation(calc));
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Calculation>), 200)]
    [ProducesResponseType(400)]
    public ActionResult<List<Calculation>> GetCalculations(int id)
    {
        try
        {
            logger.LogInformation($"Calculations requested for {id}");
            return Ok(calcService.GetCalculations(id));
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

}
