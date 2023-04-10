using MonthRates.Models;
using Lib;
using Microsoft.AspNetCore.Mvc;

namespace MonthRates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly IExchangeRateService _rateService;
        private readonly string BadMonthFormatMsg = "Bad month provided. Format is yyMM, for example 2212 for December, 2022";

        public CurrencyController(
            ILogger<CurrencyController> logger,
            IExchangeRateService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [HttpGet("{yymm}")]
        public async Task<ActionResult<MonthlyRatesResponse>> Get(string yymm)
        {
            if (string.IsNullOrEmpty(yymm))
            {
                return BadRequest(BadMonthFormatMsg);
            }
            
            var year = int.Parse("20" + yymm.Substring(0, 2));
            var month = int.Parse(yymm.Substring(2));

            DateTime startDate;

            try
            {
                startDate = new DateTime(year, month, 1);
            }
            catch (Exception)
            {
                return BadRequest(BadMonthFormatMsg);
            }
            
            var endDate = new DateTime(year, month, 1).AddMonths(1).AddSeconds(-1);

            var graph = await _rateService.GetDayRangeForRate(startDate, endDate);
            return Ok(new MonthlyRatesResponse
            {
                Graph= graph,
                Min = graph.Values.Min(),
                Max = graph.Values.Max()
            });
        }
    }
}