using Microsoft.EntityFrameworkCore;
using InventoryOps.ConsoleApp.Data;

namespace InventoryOps.ConsoleApp.Services;

public class ReportService
{
    private readonly InventoryOpsContext _context;

    public ReportService(InventoryOpsContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Top 5 kunder med flest ordrar.
    /// </summary>
    public List<(string Name, int OrderCount, decimal TotalSpent)> GetTopCustomers()
    {
        return _context.Customers
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderRows)
            .Select(c => new
            {
                c.Name,
                OrderCount = c.Orders.Count,
                TotalSpent = c.Orders
                    .SelectMany(o => o.OrderRows)
                    .Sum(r => r.Quantity * r.UnitPrice)
            })
            .OrderByDescending(x => x.OrderCount)
            .Take(5)
            .AsEnumerable()
            .Select(x => (x.Name, x.OrderCount, x.TotalSpent))
            .ToList();
    }

    /// <summary>
    /// Produkter med lågt lager (under 10).
    /// </summary>
    public List<(string ProductName, string SupplierName, int Stock, string Status)> GetLowStock(int threshold = 10)
    {
        return _context.Products
            .Include(p => p.Supplier)
            .Where(p => p.StockQuantity < threshold)
            .OrderBy(p => p.StockQuantity)
            .AsEnumerable()
            .Select(p => (p.Name, p.Supplier.Name, p.StockQuantity, p.Status))
            .ToList();
    }

    /// <summary>
    /// Senaste 20 lagerrörelser.
    /// </summary>
    public List<(string ProductName, int ChangeQty, string Reason, DateTime CreatedAt)> GetRecentMovements(int count = 20)
    {
        return _context.StockMovements
            .Include(m => m.Product)
            .OrderByDescending(m => m.CreatedAt)
            .Take(count)
            .AsEnumerable()
            .Select(m => (m.Product.Name, m.ChangeQty, m.Reason, m.CreatedAt))
            .ToList();
    }

    /// <summary>
    /// Omsättning per produkt.
    /// </summary>
    public List<(string ProductName, int TotalQuantity, decimal TotalRevenue)> GetRevenuePerProduct()
    {
        return _context.Products
            .Include(p => p.OrderRows)
            .Select(p => new
            {
                p.Name,
                TotalQuantity = p.OrderRows.Sum(r => r.Quantity),
                TotalRevenue = p.OrderRows.Sum(r => r.Quantity * r.UnitPrice)
            })
            .OrderByDescending(x => x.TotalRevenue)
            .AsEnumerable()
            .Select(x => (x.Name, x.TotalQuantity, x.TotalRevenue))
            .ToList();
    }
}
