using Microsoft.EntityFrameworkCore;
using NorthwindDal.Model;

namespace NorthwindDalTests
{
    public class Tests
    {
        NorthwindContext context;

        [SetUp]
        public void Setup()
        {
            context = new NorthwindContext();
            context.Log = LogIt;
        }

        private void LogIt(string logString)
        {
            Console.WriteLine(logString);
        }

        [Test]
        public void GetCustomers()
        {
            var qCustomers = context.Customers;

            Assert.AreEqual(91, qCustomers.Count());
        }

        [Test]
        public void GetUSCustomers()
        {
            var qUsa = context.Customers.Where(cu => cu.Country == "USA");

            foreach (Customer item in qUsa)
            {
                Console.WriteLine($"{item.CompanyName}");
            }
        }

        [Test]
        public void GetCustomersOrders()
        {
            // Include, um JOIN zu erzeugen
            Customer alkfi = context.Customers.Include(cu => cu.Orders).ThenInclude(order => order.OrderDetails).FirstOrDefault(cu => cu.CustomerId == "ALFKI");

            foreach (Order item in alkfi.Orders)
            {
                Console.WriteLine($"{item.Id} vom {item.OrderDate:g}");
            }
        }

        [Test]
        public void GetOrderInfo()
        {
            var qOrderInfo = context.Orders
                                            //.Include($"{nameof(Order.OrderDetails)}.{nameof(OrderDetail.Product)}")
                                            //.Include("OrderDetails.Product")
                                            //.Include(od => od.OrderDetails) // nicht notwendig, weil OrderDetails im Select enthalten
                                            //.ThenInclude(od => od.Product) // notwendig, wenn Product-Infos gewünscht, dann auch obige Include() aus syntaktischen Gründen
                                            .Where(od => od.CustomerId == "ALFKI")
                                            .Select(od => new { od.Customer.CompanyName, od.OrderDetails });

            foreach (var orderInfo in qOrderInfo)
            {
                Console.WriteLine($"{orderInfo.CompanyName}:");

                foreach (var item in orderInfo.OrderDetails)
                {
                    Console.WriteLine($"{item.Quantity}, {item.Product?.ProductName}, {item.UnitPrice}");
                }
            }
        }

        [Test]
        public void LetChangeTrackerDoTheJob()
        {
            Customer alfki = context.Customers.Find("ALFKI");

            Console.WriteLine($"{alfki.Orders.Count()}"); // Order Count = 0

            // ChangeTracker "webt" beim Laden Objekte zusammen, wenn es eine Beziehung gibt
            var qOrders = context.Orders.Where(od => od.Id > 11000).ToList();

            Console.WriteLine($"{alfki.Orders.Count()}"); // Order Count = 2

            // ChangeTracker "webt" beim Laden Objekte zusammen, wenn es eine Beziehung gibt
            var qAlfkisOrders = context.Orders.Where(od => od.CustomerId == "ALFKI").ToList();

            Console.WriteLine($"{alfki.Orders.Count()}"); // Order Count = 6

        }

        [Test]
        public void ChangeCustomer()
        {
            Customer alfki = context.Customers.Find("ALFKI");

            Console.WriteLine($"Status nach Load: {context.Entry(alfki).State}");

            alfki.ContactName = "Maria Maier";

            Console.WriteLine($"Status nach Namesänderung: {context.Entry(alfki).State}");

            try
            {
                context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Exception Handling für Concurrency

                // Database wins
                //context.Entry(alfki).Reload();

                // Client wins
                context.Entry(alfki).OriginalValues.SetValues(context.Entry(alfki).GetDatabaseValues());
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            Console.WriteLine($"Status nach Speichern: {context.Entry(alfki).State}");
        }

        [Test]
        public void LoadWithoutTracking()
        {
            var qCustomersUntracked = context.Customers.AsNoTracking();

            Assert.AreEqual(91, qCustomersUntracked.Count());

            Customer alfki = qCustomersUntracked.First();
            Console.WriteLine($"{context.Entry(alfki).State}");

            // ChangeTracker "webt" beim Laden Objekte zusammen, wenn es eine Beziehung gibt
            var qOrders = context.Orders.Where(od => od.Id > 11000).ToList();

            Console.WriteLine($"{alfki.Orders.Count()}"); // Order Count = 0, weil Customers ohne Tracking

            // ALFKI jetzt doch tracken
            context.Customers.Attach(alfki);
            
            Console.WriteLine($"{context.Entry(alfki).State}");
            
            alfki.ContactName = "Mike Brinkmeier";

            Console.WriteLine($"{context.Entry(alfki).State}");



            context.SaveChanges();
        }
    }
}