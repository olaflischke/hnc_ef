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
        public TradingDay()
        {

        }

        public TradingDay(XElement tradingDayNode)
        {
            this.Date = Convert.ToDateTime(tradingDayNode.Attribute("time").Value); // DateOnly.Parse(tradingDayNode.Attribute("time").Value);

            this.Currencies = tradingDayNode.Elements()
                                            .Select(el => new Currency()
                                            {
                                                Symbol = el.Attribute("currency").Value,
                                                Rate = Convert.ToDouble(el.Attribute("rate").Value, CultureInfo.InvariantCulture),
                                                TradingDay = this
                                            })
                                            .ToList();
        }

        public DateTime Date { get; set; }
        public List<Currency> Currencies { get; set; } = new();
        public string TradingLocation { get; set; }

        public int Id { get; set; }
    }
}
