using InventoryOps.ConsoleApp.Helpers;
using InventoryOps.ConsoleApp.Services;

namespace InventoryOps.ConsoleApp.Menus;

public class ReportMenu
{
    private readonly ReportService _service;

    public ReportMenu(ReportService service)
    {
        _service = service;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("Rapporter");
            Console.WriteLine("  1. Top 5 kunder (flest ordrar)");
            Console.WriteLine("  2. Lågt lager (under 10)");
            Console.WriteLine("  3. Senaste 20 lagerrörelser");
            Console.WriteLine("  4. Omsättning per produkt");
            Console.WriteLine("  0. Tillbaka");

            switch (ConsoleHelper.ReadMenuChoice())
            {
                case 1: TopCustomers(); break;
                case 2: LowStock(); break;
                case 3: RecentMovements(); break;
                case 4: RevenuePerProduct(); break;
                case 0: return;
                default:
                    ConsoleHelper.PrintError("Ogiltigt val.");
                    break;
            }
        }
    }

    private void TopCustomers()
    {
        ConsoleHelper.PrintSubHeader("Top 5 kunder");
        var data = _service.GetTopCustomers();

        if (data.Count == 0)
        {
            ConsoleHelper.PrintInfo("Ingen data.");
        }
        else
        {
            Console.WriteLine($"  {"#",-3} {"Kund",-25} {"Ordrar",7} {"Totalt",12}");
            Console.WriteLine("  " + new string('-', 50));
            for (int i = 0; i < data.Count; i++)
            {
                var (name, count, total) = data[i];
                Console.WriteLine($"  {i + 1,-3} {name,-25} {count,7} {total,12:N2} kr");
            }
        }

        ConsoleHelper.Pause();
    }

    private void LowStock()
    {
        ConsoleHelper.PrintSubHeader("Produkter med lågt lager (< 10)");
        var data = _service.GetLowStock();

        if (data.Count == 0)
        {
            ConsoleHelper.PrintInfo("Inga produkter med lågt lager.");
        }
        else
        {
            Console.WriteLine($"  {"Produkt",-20} {"Leverantör",-20} {"Lager",7} {"Status",-10}");
            Console.WriteLine("  " + new string('-', 60));
            foreach (var (product, supplier, stock, status) in data)
            {
                Console.WriteLine($"  {product,-20} {supplier,-20} {stock,7} {status,-10}");
            }
        }

        ConsoleHelper.Pause();
    }

    private void RecentMovements()
    {
        ConsoleHelper.PrintSubHeader("Senaste 20 lagerrörelser");
        var data = _service.GetRecentMovements();

        if (data.Count == 0)
        {
            ConsoleHelper.PrintInfo("Inga lagerrörelser.");
        }
        else
        {
            Console.WriteLine($"  {"Produkt",-20} {"Ändring",8} {"Orsak",-20} {"Datum",-20}");
            Console.WriteLine("  " + new string('-', 70));
            foreach (var (product, change, reason, date) in data)
            {
                Console.WriteLine($"  {product,-20} {change,8} {reason,-20} {date:yyyy-MM-dd HH:mm}");
            }
        }

        ConsoleHelper.Pause();
    }

    private void RevenuePerProduct()
    {
        ConsoleHelper.PrintSubHeader("Omsättning per produkt");
        var data = _service.GetRevenuePerProduct();

        if (data.Count == 0)
        {
            ConsoleHelper.PrintInfo("Ingen data.");
        }
        else
        {
            Console.WriteLine($"  {"Produkt",-25} {"Sålt antal",10} {"Omsättning",14}");
            Console.WriteLine("  " + new string('-', 52));
            foreach (var (product, qty, revenue) in data)
            {
                Console.WriteLine($"  {product,-25} {qty,10} {revenue,14:N2} kr");
            }
        }

        ConsoleHelper.Pause();
    }
}
