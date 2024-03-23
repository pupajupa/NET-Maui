using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MauiAppAntikhovitch.Entities;

namespace MauiAppAntikhovitch.Services
{
    public class RateService:IRateService
    {
        private readonly HttpClient _httpClient;

        public RateService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        List<Rate> rates = new List<Rate>();
        public async Task<IEnumerable<Rate>> GetRates(DateTime date)
        {   
            try
            {
                var url = "https://api.nbrb.by/exrates/rates";
                var response = await _httpClient.GetAsync($"{url}?ondate={date:yyyy - MM - dd}&periodicity=0");    
                if (response.IsSuccessStatusCode)
                {
                    rates = await response.Content.ReadFromJsonAsync<List<Rate>>();
                }
                else
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                return rates;
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Error occurred while making HTTP request.", ex);
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while retrieving rates.", ex);
            }
        }
    }
}
