using Microsoft.EntityFrameworkCore;
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

            return q.ToList(); // Zugriff auf die Ergebnismenge führt Query aus!
        }

        public void SaveToDb()
        {
            DbContextOptions<TradingDayContext> opt = new DbContextOptionsBuilder<TradingDayContext>()
                                                            .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TradingDayDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                                                            .Options;

            using (TradingDayContext context = new TradingDayContext(opt))
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();  // Legt Datenbank und Schema direkt an, wenn Datenbank nicht existiert

                //context.Database.Migrate(); 

                context.TradingDays.AddRange(this.TradingDays);

                context.SaveChanges(); // Schreibt Daten in die Datenbank
            }
        }

        public List<TradingDay> TradingDays { get; set; }
    }
}
