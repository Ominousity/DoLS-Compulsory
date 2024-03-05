using Domain;
using Microsoft.AspNetCore.Mvc;

namespace SubtractionAPI;

[ApiController]
public class SubtractionController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Calculation), 200)]
    [ProducesResponseType(400)]
    public ActionResult<Calculation> Subtraction(Calculation calc)
    {

        try
        {

            return Ok(Subtract(calc));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    private Calculation Subtract(Calculation calc)
    {

        foreach (var number in calc.Numbers)
        {

            if (calc.Result == null)
            {
                calc.Result = number;
                continue;
            }

            calc.Result -= number;

        }

        calc.DateStamp = DateTime.Now;
        return calc;
    }
}
