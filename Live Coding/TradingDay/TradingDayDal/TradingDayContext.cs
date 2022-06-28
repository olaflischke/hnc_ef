using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingDayDal
{
    public class TradingDayContext : DbContext
    {
        public TradingDayContext()
        {

        }

        public TradingDayContext(DbContextOptions<TradingDayContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TradingDayDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Seeding in EFCore - existiert Datensatz bereits, wird er nicht überschrieben
            TradingDay day = new TradingDay()
            {
                Id = 9999,
                Date = DateTime.Now
            };

            modelBuilder.Entity<TradingDay>().HasData(day);
            //modelBuilder.Entity<TradingDay>().HasQueryFilter(td => td.Deleted == false);

            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    TradingDay = day,
                    Symbol = "ABC",
                    Rate = 1.0
                }
                
                );
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<TradingDay> TradingDays { get; set; }

    }
}
