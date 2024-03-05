﻿using CalcApplication;
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
        SaveCalculation(calc);

        return calcRepo.DoCalculation(calc);
    }

    public List<Calculation> GetCalculations(int id)
    {
        return calcRepo.GetCalculations(id);
    }

    private void SaveCalculation(Calculation calc)
    {
        calcRepo.SaveCalculation(calc);
    }
}
