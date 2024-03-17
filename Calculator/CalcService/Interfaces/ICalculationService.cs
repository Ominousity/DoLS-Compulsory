using Domain;

namespace CalcApplication;

public interface ICalculationService
{
    /// <summary>
    /// <param name="calc">The calculation to be performed.</param>
    /// <returns>The result of the calculation.</returns>
    Task<Calculation> DoCalculationAsync(Calculation calc);

    /// <summary>
    /// Retrieves a list of calculations for a given ID.
    /// </summary>
    /// <param name="id">The ID of the calculations to retrieve.</param>
    /// <returns>A list of calculations.</returns>d
    Task<List<Calculation>> GetCalculations(Guid id);
}
