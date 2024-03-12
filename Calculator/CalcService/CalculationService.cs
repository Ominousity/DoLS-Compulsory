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
        Calculation temp = calcRepo.DoCalculation(calc);
        SaveCalculation(temp);
        return temp;
    }

    public List<Calculation> GetCalculations(Guid id)
    {
        return calcRepo.GetCalculations(id);
    }

    private void SaveCalculation(Calculation calc)
    {
        calcRepo.SaveCalculation(calc);
    }
}
