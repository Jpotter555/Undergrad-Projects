// Jonathan Potter Ryan Hebert
// C00299690    C00299553
// CMPS 358
// project #8
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Text;

namespace p8_c00299690_c00299553.Pages;

public static class DiscontinuedItems
{
    public static string GetDiscontinuedItems(string discontinued_bound_property)
    {
        using (var db = new p8_c00299690_c00299553())
        {
            try
            {
                var results =
                    from x in db.Products
                    where x.IsDiscontinued.ToString() == "1"
                    select x.ProductName;
                string items = "These are the discontinued products: ";
                foreach (var p in results)
                    items += $"{p}, ";
                return items;
            }
            catch (Exception e)
            {
                return "Error.";
            }
        }
    }
}

public static class GetCustomers
{
    public static string GetCustomer(string Country)
    {
        using (var db = new p8_c00299690_c00299553())
        {
            try
            {
                var results =
                    from x in db.Customers
                    where x.Country == Country
                    select new {x.FirstName, x.LastName, x.Phone};
                if (!results.Any())
                {
                    return "";
                }
                string output = $"These are the customers in {Country}: ";
                foreach (var c in results)
                    output += $"Customer Name: {c.FirstName} {c.LastName} Phone Number: {c.Phone} | ";
                return output;
            }
            catch (Exception e)
            {
                return $"Error finding customers in {Country}";
            }
        }
        
    }
}

public static class GetSuppliers
{
    public static string GetSupplier(string Country)
    {
        using (var db = new p8_c00299690_c00299553())
        {
            try
            {
                var results =
                    from x in db.Suppliers
                    where x.Country == Country
                    select new {x.Id, x.CompanyName, x.Phone, x.Fax, x.City};
                
                if (!results.Any()) 
                    return "";
                string output = $"These are the suppliers in {Country}: ";
                foreach (var c in results)
                {
                    if(c.Fax != null)
                        output += $"Company ID: {c.Id} | Company Name: {c.CompanyName} | Company Phone: {c.Phone} | Company Fax: {c.Fax} | Company City: {c.City} --> ";
                    else
                        output += $"Company ID: {c.Id} | Company Name: {c.CompanyName} | Company Phone: {c.Phone} | Company Fax: N/A | Company City: {c.City} --> ";
                }

                return output;
            }
            catch (Exception e)
            {
                return $"Error finding Suppliers in {Country}";
            }
        }
    }
}

public static class ListProducts
{
    public static string ListProduct(string Supplier)
    {
        using (var db = new p8_c00299690_c00299553())
        {
            try
            {
                var results =
                    from x in db.Suppliers
                    where x.CompanyName == Supplier
                    join y in db.Products
                        on x.Id equals y.SupplierId
                    where y.IsDiscontinued.ToString() == "0"
                    select new {x.CompanyName, y.ProductName, y.UnitPrice, y.Package};

                if (!results.Any())
                    return "";
                
                string output = $"{Supplier} has these products that aren't discontinued: ";
                foreach (var p in results)
                    output += $"Company Name: {p.CompanyName} | Product Name: {p.ProductName} | Unit Price: {Encoding.UTF8.GetString(p.UnitPrice)} | Package Details: {p.Package} --> ";
                return output;
            }
            catch (Exception e)
            {
                return $"Error in finding {Supplier}";
            }
        }
    }
}

public static class OrderDetails
{
    public static string OrderDetail(string OrderNumber)
    {
        using (var db = new p8_c00299690_c00299553())
        {
            try
            {
                var results1 =
                    from x in db.Orders
                    where x.OrderNumber == OrderNumber
                    join y in db.Customers
                        on x.CustomerId equals y.Id
                    select new {x.Id, y.FirstName, y.LastName, x.TotalAmount};
                
                if (!results1.Any())
                    return "";
                
                var results2 =
                    from x in results1
                    join y in db.OrderItems
                        on x.Id equals y.OrderId
                    select new {x.FirstName, x.LastName, x.TotalAmount, y.ProductId, y.Quantity, y.UnitPrice};

                var results3 =
                    from x in results2
                    join y in db.Products
                        on x.ProductId equals y.Id
                    select new {x.FirstName, x.LastName, x.TotalAmount, y.ProductName, x.Quantity, y.UnitPrice};

                string output = $"Order Details for {OrderNumber}:";
                foreach (var p in results3)
                {
                    var d = Encoding.UTF8.GetString(p.UnitPrice);
                    var s = double.Parse(d);
                    
                    output += $" Customer Name: {p.FirstName} {p.LastName} | Product Name: {p.ProductName} | Quantity: {p.Quantity} | Unit Price: {Encoding.UTF8.GetString(p.UnitPrice)} | Sub Total: {p.Quantity * s} --> ";
                }
                    
                return output;
            }
            catch (Exception e)
            {
                return $"Error in finding the order number {OrderNumber}";
            }
        }
    }
}
public class IndexModel : PageModel
{
    [BindProperty]
    public string discontinued_bound_property { get; set; }
    [BindProperty]
    public string Country { get; set; }
    [BindProperty]
    public string Country2 { get; set; }
    [BindProperty]
    public string Supplier { get; set; }
    [BindProperty]
    public string OrderNumber { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}