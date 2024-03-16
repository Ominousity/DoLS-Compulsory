namespace Domain;

/// <summary>
/// Represents a calculation operation.
/// </summary>
public class Calculation
{
    /// <summary>
    /// Gets or sets the calculation ID.
    /// </summary>
    public int CalculationId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the equation used for the calculation.
    /// </summary>
    public string? Equation { get; set; }

    /// <summary>
    /// Gets or sets the list of numbers.
    /// </summary>
    public List<float>? Numbers { get; set; }

    /// <summary>
    /// Gets or sets the operator used in the calculation.
    /// </summary>
    public Operator Operation { get; set; }

    /// <summary>
    /// Gets or sets the result of the calculation.
    /// </summary>
    public float? Result { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the calculation was performed.
    /// </summary>
    public DateTime? DateStamp { get; set; }
}

/// <summary>
/// Represents the mathematical operators that can be used in calculations.
/// </summary>
public enum Operator
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}