namespace InventoryOps.ConsoleApp.Models;

public class StockMovement
{
    public int MovementId { get; set; }
    public int ProductId { get; set; }
    public int ChangeQty { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Product Product { get; set; } = null!;
}
