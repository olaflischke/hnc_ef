using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingDayDal;

namespace TradingDayDalUnitTests
{
    public class DbTests
    {
        [Test]
        public void IsArchiveSavingToDb()
        {
            Archive archive = new Archive("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml");
            archive.SaveToDb();

            Assert.Pass();
        }
    }
}
