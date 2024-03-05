using Microsoft.AspNetCore.Mvc;
using Domain;
using CalcApplication;

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
    public ActionResult<Calculation> Get(Calculation calc)
    {
        try
        {
            return Ok(calcService.DoCalculation(calc));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
