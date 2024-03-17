using Domain;

namespace CalcApplication;

/// <summary>
/// Represents a repository for performing calculations.
/// </summary>
public interface ICalculationRepository
{
    /// <summary>
    /// <param name="calc">The calculation to be performed.</param>
    /// <returns>The result of the calculation.</returns>
    Task<float> DoCalculation(Operator @operator, float num1, float num2);

    /// <summary>
    /// Retrieves a list of calculations for a given ID.
    /// </summary>
    /// <param name="id">The ID of the calculations to retrieve.</param>
    /// <returns>A list of calculations.</returns>
    Task<List<Calculation>> GetCalculations(Guid id);

    /// <summary>
    /// Saves a calculation.
    /// </summary>
    /// <param name="calc">The calculation to be saved.</param>
    Task SaveCalculation(Calculation calc);
}
