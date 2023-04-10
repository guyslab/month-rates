using System.Text.Json.Serialization;

namespace MonthRates.Models
{
    public class MonthlyRatesResponse
    {
        [JsonPropertyName("GRAPH")]
        public IDictionary<string, decimal> Graph { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}
