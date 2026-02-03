using InventoryOps.ConsoleApp.Helpers;
using InventoryOps.ConsoleApp.Services;

namespace InventoryOps.ConsoleApp.Menus;

public class ProductMenu
{
    private readonly ProductService _service;

    public ProductMenu(ProductService service)
    {
        _service = service;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("Produkter");
            Console.WriteLine("  1. Lista alla produkter");
            Console.WriteLine("  2. Ändra status (Active/Inactive)");
            Console.WriteLine("  0. Tillbaka");

            switch (ConsoleHelper.ReadMenuChoice())
            {
                case 1: ListAll(); break;
                case 2: UpdateStatus(); break;
                case 0: return;
                default:
                    ConsoleHelper.PrintError("Ogiltigt val.");
                    break;
            }
        }
    }

    private void ListAll()
    {
        ConsoleHelper.PrintSubHeader("Alla produkter");
        var products = _service.GetAll();

        if (products.Count == 0)
        {
            ConsoleHelper.PrintInfo("Inga produkter hittades.");
        }
        else
        {
            Console.WriteLine($"  {"ID",-5} {"Namn",-20} {"Leverantör",-20} {"Pris",10} {"Lager",7} {"Status",-10}");
            Console.WriteLine("  " + new string('-', 75));
            foreach (var p in products)
            {
                Console.WriteLine($"  {p.ProductId,-5} {p.Name,-20} {p.Supplier.Name,-20} {p.UnitPrice,10:N2} {p.StockQuantity,7} {p.Status,-10}");
            }
        }

        ConsoleHelper.Pause();
    }

    private void UpdateStatus()
    {
        ConsoleHelper.PrintSubHeader("Ändra produktstatus");

        var id = ConsoleHelper.ReadInt("  Produkt-ID: ");
        var product = _service.GetById(id);

        if (product == null)
        {
            ConsoleHelper.PrintError("Produkten hittades inte.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine($"  Produkt: {product.Name} (nuvarande status: {product.Status})");
        var newStatus = product.Status == "Active" ? "Inactive" : "Active";

        if (!ConsoleHelper.Confirm($"  Ändra till {newStatus}?"))
        {
            ConsoleHelper.PrintInfo("Avbrutet.");
            ConsoleHelper.Pause();
            return;
        }

        _service.UpdateStatus(id, newStatus);
        ConsoleHelper.PrintSuccess($"Status ändrad till {newStatus}.");
        ConsoleHelper.Pause();
    }
}
