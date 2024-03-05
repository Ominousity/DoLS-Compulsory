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
    Calculation DoCalculation(Calculation calc);

    /// <summary>
    /// Retrieves a list of calculations for a given ID.
    /// </summary>
    /// <param name="id">The ID of the calculations to retrieve.</param>
    /// <returns>A list of calculations.</returns>
    List<Calculation> GetCalculations(int id);

    /// <summary>
    /// Saves a calculation.
    /// </summary>
    /// <param name="calc">The calculation to be saved.</param>
    void SaveCalculation(Calculation calc);
}
