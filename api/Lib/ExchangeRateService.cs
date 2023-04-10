using Lib.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _jsonOptions;

        private const string ServiceDateFormat = "yyyy-MM-dd";
        private const string DayRangeEndpointPath = "/exchangerates_data/timeseries";

        public ExchangeRateService(HttpClient client)
        {
            _client= client;            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive=true
            };
        }
    
        public async Task<IDictionary<string, decimal>> GetDayRangeForRate(DateTime startDate, DateTime endDate, string rate)
        {
            if (startDate > endDate) 
            {
                throw new ArgumentException("start date cannot be after end date");
            }

            var symbols = rate.Split('/');
            var baseCurr = symbols[1];
            var targetCurr = symbols[0];
            var formattedStartDate = startDate.ToString(ServiceDateFormat);
            var formattedEndDate = endDate.ToString(ServiceDateFormat);
            var path = DayRangeEndpointPath;
            var query = $"?&start_date={formattedStartDate}&end_date={formattedEndDate}&base={baseCurr}&symbols={targetCurr}";

            TimeseriesServiceResponse serviceResponse;
            try
            {
                using var jsonStream = await _client.GetStreamAsync(path + query);
                serviceResponse = await JsonSerializer.DeserializeAsync<TimeseriesServiceResponse>(jsonStream, _jsonOptions);
            }
            catch (HttpRequestException e)
            {
                throw new ApplicationException("Error fetching from external service", e);
            }

            var result = serviceResponse.Rates.ToDictionary(kv => kv.Key, kv => kv.Value[targetCurr]);

            return result;
        }
    }
}
