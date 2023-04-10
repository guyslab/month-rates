using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public interface IExchangeRateService
    {
        Task<IDictionary<string, decimal>> GetDayRangeForRate(DateTime startDate, DateTime endDate, string rate = Constants.USD_ILS);
    }
}
