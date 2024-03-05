namespace Domain;

public class Calculation
{
    public Guid UserId { get; set; }
    public List<float>? Numbers { get; set; }
    Operator Operation { get; set; }
    public float? Result { get; set; }
    public DateTime? DateStamp { get; set; }
}

public enum Operator
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}