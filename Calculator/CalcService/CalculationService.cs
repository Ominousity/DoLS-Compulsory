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

    public async Task<Calculation> DoCalculationAsync(Calculation calc)
    {
        calc.DateStamp = DateTime.Now;
        calc.Result = await EvaluateExpressionAsync(calc.Equation);
        await SaveCalculation(calc);
        return calc;
    }

    public async Task<List<Calculation>> GetCalculations(Guid id)
    {
        return await calcRepo.GetCalculations(id);
    }

    private async Task SaveCalculation(Calculation calc)
    {
        await calcRepo.SaveCalculation(calc);
    }

    private async Task<float> CallOperatorsAsync(Operator @operator, float num1, float num2)
    {
        return await calcRepo.DoCalculation(@operator, num1, num2);
    }

    private async Task<float> EvaluateExpressionAsync(string expression)
    {
        for (int i = 0; i < expression.Length - 1; i++)
        {
            if ((char.IsDigit(expression[i]) || expression[i] == ')') && expression[i + 1] == '(')
            {
                expression = expression.Insert(i + 1, "*");
            }
        }

        expression = expression.Replace(" ", ""); // Remove spaces for simplicity
        Stack<float> operands = new Stack<float>();
        Stack<char> operators = new Stack<char>();

        for (int i = 0; i < expression.Length; i++)
        {
            char currentChar = expression[i];
            if (char.IsDigit(currentChar))
            {
                string operand = currentChar.ToString();
                while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                {
                    operand += expression[i + 1];
                    i++;
                }
                operands.Push(float.Parse(operand));
            }
            else if (currentChar == '(')
            {
                operators.Push(currentChar);
            }
            else if (currentChar == ')')
            {
                while (operators.Peek() != '(')
                {
                    await ProcessOperatorAsync(operands, operators);
                }
                operators.Pop(); // Discard the opening parenthesis
            }
            else if (IsOperator(currentChar))
            {
                while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(currentChar))
                {
                    await ProcessOperatorAsync(operands, operators);
                }
                operators.Push(currentChar);
            }
        }

        while (operators.Count > 0)
        {
            await ProcessOperatorAsync(operands, operators);
        }

        return operands.Pop();
    }

    private async Task ProcessOperatorAsync(Stack<float> operands, Stack<char> operators)
    {
        float operand2 = operands.Pop();
        float operand1 = operands.Pop();
        char operation = operators.Pop();

        float result = await PerformOperationAsync(operand1, operand2, operation);
        operands.Push(result);
    }

    private async Task<float> PerformOperationAsync(float operand1, float operand2, char operation)
    {
        switch (operation)
        {
            case '+':
                return await CallOperatorsAsync(Operator.Addition, operand1, operand2);
            case '-':
                return await CallOperatorsAsync(Operator.Subtraction, operand1, operand2);
            case '*':
                return await CallOperatorsAsync(Operator.Multiplication, operand1, operand2);
            case '/':
                if (operand2 == 0)
                    throw new DivideByZeroException("Cannot divide by zero.");
                return await CallOperatorsAsync(Operator.Division, operand1, operand2);
            default:
                throw new ArgumentException("Invalid operation.");
        }
    }

    private bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/';
    }

    private int Precedence(char op)
    {
        if (op == '+' || op == '-')
            return 1;
        else if (op == '*' || op == '/')
            return 2;
        else
            return 0; // Parentheses have the highest precedence
    }
}
