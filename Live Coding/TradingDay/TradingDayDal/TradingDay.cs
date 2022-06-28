using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TradingDayDal
{
    public class TradingDay
    {
        public TradingDay(XElement tradingDayNode)
        {
            this.Date = DateOnly.Parse(tradingDayNode.Attribute("time").Value);

            this.ExchangeRates = tradingDayNode.Elements()
                                            .Select(el => new ExchangeRate()
                                            {
                                                Symbol = el.Attribute("currency").Value,
                                                Rate = Convert.ToDouble(el.Attribute("rate").Value, CultureInfo.InvariantCulture)
                                            })
                                            .ToList();
        }

        public DateOnly Date { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; } = new();
    }
}
