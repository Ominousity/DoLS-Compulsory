using System.Net.Http.Headers;
using System.Net.Http.Json;
using CalcApplication;
using Domain;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CalcInfrastruture;

public class CalculationRepository : ICalculationRepository
{
    public async Task<float> DoCalculation(Operator @operator, float num1, float num2)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri($"http://{@operator}:8080");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Log.Logger.Information("Sending calculation to server");
            HttpResponseMessage response = client.GetAsync($"/do{@operator}?a={num1}&b={num2}").Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<float>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

    public List<Calculation> GetCalculations(Guid id)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://memory:8080");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Log.Logger.Information("Getting calculations from server");
            HttpResponseMessage response = client.GetAsync("/getCalculations?UserId=" + id).Result;
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
            client.BaseAddress = new Uri("http://memory:8080");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Log.Logger.Information("Saving calculation to server");
            HttpResponseMessage response = client.PostAsJsonAsync("/saveCalculation", calc).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
