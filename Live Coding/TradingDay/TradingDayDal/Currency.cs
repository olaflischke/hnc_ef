using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingDayDal
{
    //[Table("ExchangeRates")]
    public class Currency
    {
        public string Symbol { get; set; }
        public double Rate { get; set; }

        [Key]
        public int Id { get; set; } //= Guid.NewGuid();

        public TradingDay TradingDay { get; set; }
    }
}