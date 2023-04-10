using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Models
{
    public class TimeseriesServiceResponse
    {
        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }
    }
}
