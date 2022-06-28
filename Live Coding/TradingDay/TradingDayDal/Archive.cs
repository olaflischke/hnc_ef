using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TradingDayDal
{
    public class Archive
    {

        public Archive(string url)
        {
            this.TradingDays = GetData(url);
        }

        private List<TradingDay>? GetData(string url)
        {
            XDocument document = XDocument.Load(url);

            var q = document.Root.Descendants()
                                .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Any(at => at.Name == "time"))
                                .Select(xe => new TradingDay(xe));

            return q.ToList();
        }

        public void SaveToDb()
        {

        }

        public List<TradingDay> TradingDays { get; set; }
    }
}
