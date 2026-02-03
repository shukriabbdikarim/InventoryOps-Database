using Microsoft.EntityFrameworkCore;
using InventoryOps.ConsoleApp.Data;
using InventoryOps.ConsoleApp.Models;

namespace InventoryOps.ConsoleApp.Services;

public class OrderService
{
    private readonly InventoryOpsContext _context;

    public OrderService(InventoryOpsContext context)
    {
        _context = context;
    }

    public List<Order> GetAll()
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderRows)
                .ThenInclude(r => r.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
    }

    public Order? GetById(int id)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderRows)
                .ThenInclude(r => r.Product)
            .FirstOrDefault(o => o.OrderId == id);
    }

    public Order CreateOrder(int customerId)
    {
        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.Now
        };
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

    public OrderRow AddOrderRow(int orderId, int productId, int quantity)
    {
        var product = _context.Products.Find(productId)
            ?? throw new InvalidOperationException("Produkten finns inte.");

        var row = new OrderRow
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = product.UnitPrice
        };
        _context.OrderRows.Add(row);

        // Uppdatera lagersaldo
        product.StockQuantity -= quantity;

        // Logga lagerrörelse
        _context.StockMovements.Add(new StockMovement
        {
            ProductId = productId,
            ChangeQty = -quantity,
            Reason = $"Order #{orderId}",
            CreatedAt = DateTime.Now
        });

        _context.SaveChanges();
        return row;
    }

    public bool CustomerExists(int customerId)
    {
        return _context.Customers.Any(c => c.CustomerId == customerId);
    }

    public bool OrderExists(int orderId)
    {
        return _context.Orders.Any(o => o.OrderId == orderId);
    }
}
