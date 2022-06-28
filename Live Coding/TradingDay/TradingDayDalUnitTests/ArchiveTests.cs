using TradingDayDal;

namespace TradingDayDalUnitTests
{
    public class Tests
    {
        string url;

        [SetUp]
        public void Setup()
        {
            url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
            //url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml";
        }

        [Test]
        public void IsArchiveInitializing()
        {
            Archive archive=new Archive(url);

            Console.WriteLine($"USD vom {archive.TradingDays?.FirstOrDefault()?.Date:dd.MM.yy}: {archive.TradingDays?.FirstOrDefault()?.Currencies?.Where(er => er.Symbol == "USD").FirstOrDefault()?.Rate}");

            Assert.AreEqual(62, archive.TradingDays.Count);
        }

    }
}