namespace InventoryOps.ConsoleApp.Models;

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
}
