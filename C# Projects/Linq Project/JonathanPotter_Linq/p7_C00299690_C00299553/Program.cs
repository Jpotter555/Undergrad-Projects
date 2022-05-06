// Jonathan Potter and Ryan Hebert
// C00299690 C00299553
// CMPS 358
// project #7

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace p7_C00299690_C00299553
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.Write("Enter a number 1-5 to pick a function or enter 0 to exit: ");
            var input = Console.ReadLine();
            while (input != "0")
            {
                switch (input)
                {
                    case "1": 
                        Discontinued(); 
                        break;
                    case "2":
                        Console.Write("Please input a country name: ");
                        var Country = Console.ReadLine();
                        Number2(Country);
                        break; 
                    case "3":
                        Console.Write("Please input a country: ");
                        var Country2 = Console.ReadLine();
                        Number3(Country2);
                        break;
                    case "4":
                        Console.Write("Please input a supplier: ");
                        var Supplier = Console.ReadLine();
                        Number4(Supplier);
                        break;
                    case "5":
                        Console.Write("Please give a number between 542378 and 543207: ");
                        var OrderNum = Console.ReadLine();
                        Number5(OrderNum);
                        break;
                }

                if (input == "0")
                    break;
                Console.Write("Enter a number 1-5 to pick a function or enter 0 to exit: ");
                input = Console.ReadLine();
            }
        }
        
        static void Discontinued()
        {
            using (var db = new p7_C00299690_C00299553())
            {
                var results =
                    from x in db.Product
                    where x.IsDiscontinued.ToString() == "1"
                    select x.ProductName;
                if (results.Count() == 0)
                {
                    Console.WriteLine("There are no discontinued products.");
                    return;
                }
                Console.WriteLine("These are the discontinued products: ");
                foreach (var p in results)
                    Console.WriteLine($"{p} ");
                Console.WriteLine();
            }
        }

        static void Number2(string Country)
        {
            using (var db = new p7_C00299690_C00299553())
            {
                var results =
                    from x in db.Customer
                    where x.Country == Country
                    select new {x.FirstName, x.LastName, x.Phone};
                if (results.Count() == 0)
                {
                    Console.WriteLine($"There are no customers in {Country}.");
                    return;
                }
                Console.WriteLine($"These are the customers in {Country}:");
                foreach (var c in results)
                    Console.WriteLine($"Customer Name: {c.FirstName} {c.LastName} | Phone Number: {c.Phone}");
                Console.WriteLine();
            }
        }

        static void Number3(string Country)
        {
            using (var db = new p7_C00299690_C00299553())
            {
                var results =
                    from x in db.Supplier
                    where x.Country == Country
                    select new {x.Id, x.CompanyName, x.Phone, x.Fax, x.City};
                
                if (results.Count() == 0)
                {
                    Console.WriteLine($"There are no suppliers in {Country}.");
                    return;
                }
                Console.WriteLine($"These are the suppliers in {Country}:");
                foreach (var c in results)
                {
                    if(c.Fax != null)
                        Console.WriteLine($"Company ID: {c.Id} | Company Name: {c.CompanyName} | Company Phone: {c.Phone} | Company Fax: {c.Fax} | Company City: {c.City}");
                    else
                        Console.WriteLine($"Company ID: {c.Id} | Company Name: {c.CompanyName} | Company Phone: {c.Phone} | Company Fax: N/A | Company City: {c.City}");
                    
                }
                    
                Console.WriteLine();
            }
        }

        static void Number4(string Supplier)
        {
            using (var db = new p7_C00299690_C00299553())
            {
                var results =
                    from x in db.Supplier
                    where x.CompanyName == Supplier
                    join y in db.Product
                        on x.Id equals y.SupplierId
                    where y.IsDiscontinued.ToString() == "0"
                    select new {x.CompanyName, y.ProductName, y.UnitPrice, y.Package};

                if (results.Count() == 0)
                {
                    Console.WriteLine($"There are no non-discontinued products from {Supplier}");
                    return;
                }
                Console.WriteLine($"{Supplier} has these products that aren't discontinued: ");
                foreach (var p in results)
                    Console.WriteLine($"Company Name: {p.CompanyName} | Product Name: {p.ProductName} | Unit Price: {Encoding.UTF8.GetString(p.UnitPrice)} | Package Details: {p.Package}");
                Console.WriteLine();
            }
        }

        static void Number5(string OrderNumber)
        {
            using (var db = new p7_C00299690_C00299553())
            {
                var results1 =
                    from x in db.Order
                    where x.OrderNumber == OrderNumber
                    join y in db.Customer
                        on x.CustomerId equals y.Id
                    select new {x.Id, y.FirstName, y.LastName, x.TotalAmount};
                
                if (results1.Count() == 0)
                {
                    Console.WriteLine($"There is no order number {OrderNumber}.");
                    return;
                }
                
                var results2 =
                    from x in results1
                    join y in db.OrderItem
                        on x.Id equals y.OrderId
                    select new {x.FirstName, x.LastName, x.TotalAmount, y.ProductId, y.Quantity, y.UnitPrice};

                var results3 =
                    from x in results2
                    join y in db.Product
                        on x.ProductId equals y.Id
                    select new {x.FirstName, x.LastName, x.TotalAmount, y.ProductName, x.Quantity, y.UnitPrice};

                Console.WriteLine($"Order Number {OrderNumber}:");
                foreach(var p in results3)
                    Console.WriteLine($"Customer Name: {p.FirstName} {p.LastName} | Sub Total: {Encoding.UTF8.GetString(p.TotalAmount)} | Product Name: {p.ProductName} | Quantity: {p.Quantity} | Unit Price: {Encoding.UTF8.GetString(p.UnitPrice)}");
            }
        }
    }
}


