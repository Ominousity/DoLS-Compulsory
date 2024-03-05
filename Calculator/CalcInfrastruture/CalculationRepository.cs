﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using CalcApplication;
using Domain;
using Microsoft.Extensions.Logging;

namespace CalcInfrastruture;

public class CalculationRepository : ICalculationRepository
{
    private readonly ILogger<CalculationRepository> _logger;

    public CalculationRepository(ILogger<CalculationRepository> logger)
    {
        _logger = logger;
    }
    public Calculation DoCalculation(Calculation calc)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger.LogInformation("Sending calculation to server");
            HttpResponseMessage response = client.PostAsJsonAsync(calc.Operation.ToString(), calc).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Calculation>().Result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

    public List<Calculation> GetCalculations(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger.LogInformation("Getting calculations from server");
            HttpResponseMessage response = client.GetAsync("getCalculations/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<Calculation>>().Result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

    public void SaveCalculation(Calculation calc)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger.LogInformation("Saving calculation to server");
            HttpResponseMessage response = client.PostAsJsonAsync("saveCalculation", calc).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
