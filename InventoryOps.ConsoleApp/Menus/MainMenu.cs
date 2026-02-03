using InventoryOps.ConsoleApp.Helpers;

namespace InventoryOps.ConsoleApp.Menus;

public class MainMenu
{
    private readonly CustomerMenu _customerMenu;
    private readonly ProductMenu _productMenu;
    private readonly OrderMenu _orderMenu;
    private readonly ReportMenu _reportMenu;

    public MainMenu(CustomerMenu customerMenu, ProductMenu productMenu,
                    OrderMenu orderMenu, ReportMenu reportMenu)
    {
        _customerMenu = customerMenu;
        _productMenu = productMenu;
        _orderMenu = orderMenu;
        _reportMenu = reportMenu;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("InventoryOps");
            Console.WriteLine("  1. Kunder");
            Console.WriteLine("  2. Produkter");
            Console.WriteLine("  3. Ordrar");
            Console.WriteLine("  4. Rapporter");
            Console.WriteLine("  0. Avsluta");

            switch (ConsoleHelper.ReadMenuChoice())
            {
                case 1: _customerMenu.Show(); break;
                case 2: _productMenu.Show(); break;
                case 3: _orderMenu.Show(); break;
                case 4: _reportMenu.Show(); break;
                case 0: return;
                default:
                    ConsoleHelper.PrintError("Ogiltigt val.");
                    break;
            }
        }
    }
}
