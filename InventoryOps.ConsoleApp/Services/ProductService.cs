using Microsoft.EntityFrameworkCore;
using InventoryOps.ConsoleApp.Data;
using InventoryOps.ConsoleApp.Models;

namespace InventoryOps.ConsoleApp.Services;

public class ProductService
{
    private readonly InventoryOpsContext _context;

    public ProductService(InventoryOpsContext context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        return _context.Products
            .Include(p => p.Supplier)
            .OrderBy(p => p.Name)
            .ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products
            .Include(p => p.Supplier)
            .FirstOrDefault(p => p.ProductId == id);
    }

    public List<Product> GetActive()
    {
        return _context.Products
            .Include(p => p.Supplier)
            .Where(p => p.Status == "Active")
            .OrderBy(p => p.Name)
            .ToList();
    }

    public bool UpdateStatus(int id, string newStatus)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;

        product.Status = newStatus;
        _context.SaveChanges();
        return true;
    }
}
