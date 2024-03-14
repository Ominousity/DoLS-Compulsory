using System.Text.RegularExpressions;
using CalcApplication;
using Domain;

namespace CalcService;

public class CalculationService : ICalculationService
{
    private readonly ICalculationRepository calcRepo;


    public CalculationService(ICalculationRepository _calcRepo)
    {
        calcRepo = _calcRepo;
    }

    public Calculation DoCalculation(Calculation calc)
    {
        Calculation finishedCalculation = SplitEquation(calc);
        SaveCalculation(finishedCalculation);
        return finishedCalculation;
    }

    public List<Calculation> GetCalculations(Guid id)
    {
        return calcRepo.GetCalculations(id);
    }

    private void SaveCalculation(Calculation calc)
    {
        calcRepo.SaveCalculation(calc);
    }

    private Calculation CallOperators(Calculation calc)
    {
        return calcRepo.DoCalculation(calc);
    }

    private Calculation SplitEquation(Calculation calc)
    {
        string pattern = @"(\d+)\s*([+\-*/])\s*(\d+)";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(calc.Equation);

        Stack<float> numbers = new Stack<float>();
        Stack<string> operations = new Stack<string>();

        foreach (Match match in matches)
        {
            float number1 = numbers.Pop();
            float number2 = float.Parse(match.Groups[1].Value);
            string operation = match.Groups[2].Value;

            if (operation == "*" || operation == "/")
            {
                float? result = operation == "*"
                    ? CallOperators(new Calculation { Numbers = new List<float> { number1, number2 }, Operation = Operator.Multiplication }).Result
                    : CallOperators(new Calculation { Numbers = new List<float> { number1, number2 }, Operation = Operator.Division }).Result;
                numbers.Push(result.Value);
            }
            else
            {
                operations.Push(operation);
                numbers.Push(number1);
                numbers.Push(number2);
            }
        }

        while (operations.Count > 0)
        {
            float number2 = numbers.Pop();
            float number1 = numbers.Pop();
            string operation = operations.Pop();

            float? result = operation == "+"
                ? CallOperators(new Calculation { Numbers = new List<float> { number1, number2 }, Operation = Operator.Addition }).Result
                : CallOperators(new Calculation { Numbers = new List<float> { number1, number2 }, Operation = Operator.Subtraction }).Result;
            numbers.Push(result.Value);
        }

        float finalResult = numbers.Pop();

        Calculation finishedCalculation = new Calculation
        {
            UserId = calc.UserId,
            Equation = calc.Equation,
            Operation = calc.Operation,
            Result = finalResult,
            DateStamp = DateTime.Now
        };
        return finishedCalculation;
    }
}
