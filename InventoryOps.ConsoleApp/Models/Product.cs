namespace InventoryOps.ConsoleApp.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public string Status { get; set; } = "Active";

    public Supplier Supplier { get; set; } = null!;
    public ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
