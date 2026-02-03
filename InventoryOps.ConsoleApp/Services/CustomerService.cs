using Microsoft.EntityFrameworkCore;
using InventoryOps.ConsoleApp.Data;
using InventoryOps.ConsoleApp.Models;

namespace InventoryOps.ConsoleApp.Services;

public class CustomerService
{
    private readonly InventoryOpsContext _context;

    public CustomerService(InventoryOpsContext context)
    {
        _context = context;
    }

    public List<Customer> GetAll()
    {
        return _context.Customers
            .OrderBy(c => c.Name)
            .ToList();
    }

    public Customer? GetById(int id)
    {
        return _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefault(c => c.CustomerId == id);
    }

    public void Create(string name, string email)
    {
        var customer = new Customer { Name = name, Email = email };
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public bool Update(int id, string name, string email)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return false;

        customer.Name = name;
        customer.Email = email;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var customer = _context.Customers
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderRows)
            .FirstOrDefault(c => c.CustomerId == id);

        if (customer == null) return false;

        // Ta bort orderrader och ordrar först (FK-beroende)
        foreach (var order in customer.Orders)
        {
            _context.OrderRows.RemoveRange(order.OrderRows);
        }
        _context.Orders.RemoveRange(customer.Orders);
        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return true;
    }

    public bool EmailExists(string email, int? excludeId = null)
    {
        return _context.Customers
            .Any(c => c.Email == email && (excludeId == null || c.CustomerId != excludeId));
    }
}
