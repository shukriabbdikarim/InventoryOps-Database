using InventoryOps.ConsoleApp.Helpers;
using InventoryOps.ConsoleApp.Services;

namespace InventoryOps.ConsoleApp.Menus;

public class OrderMenu
{
    private readonly OrderService _orderService;
    private readonly ProductService _productService;

    public OrderMenu(OrderService orderService, ProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("Ordrar");
            Console.WriteLine("  1. Lista alla ordrar");
            Console.WriteLine("  2. Skapa ny order");
            Console.WriteLine("  3. Lägg till orderrad");
            Console.WriteLine("  0. Tillbaka");

            switch (ConsoleHelper.ReadMenuChoice())
            {
                case 1: ListAll(); break;
                case 2: CreateOrder(); break;
                case 3: AddOrderRow(); break;
                case 0: return;
                default:
                    ConsoleHelper.PrintError("Ogiltigt val.");
                    break;
            }
        }
    }

    private void ListAll()
    {
        ConsoleHelper.PrintSubHeader("Alla ordrar");
        var orders = _orderService.GetAll();

        if (orders.Count == 0)
        {
            ConsoleHelper.PrintInfo("Inga ordrar hittades.");
        }
        else
        {
            foreach (var o in orders)
            {
                var total = o.OrderRows.Sum(r => r.Quantity * r.UnitPrice);
                Console.WriteLine($"  Order #{o.OrderId}  |  {o.Customer.Name,-20}  |  {o.OrderDate:yyyy-MM-dd}  |  {total,10:N2} kr");

                foreach (var r in o.OrderRows)
                {
                    Console.WriteLine($"    - {r.Product.Name,-20}  {r.Quantity} st x {r.UnitPrice:N2} = {r.Quantity * r.UnitPrice:N2} kr");
                }
            }
        }

        ConsoleHelper.Pause();
    }

    private void CreateOrder()
    {
        ConsoleHelper.PrintSubHeader("Skapa ny order");

        var customerId = ConsoleHelper.ReadInt("  Kund-ID: ");

        if (!_orderService.CustomerExists(customerId))
        {
            ConsoleHelper.PrintError("Kunden finns inte.");
            ConsoleHelper.Pause();
            return;
        }

        var order = _orderService.CreateOrder(customerId);
        ConsoleHelper.PrintSuccess($"Order #{order.OrderId} skapad.");

        // Fråga om användaren vill lägga till rader direkt
        while (ConsoleHelper.Confirm("  Lägg till en orderrad?"))
        {
            AddRowToOrder(order.OrderId);
        }

        ConsoleHelper.Pause();
    }

    private void AddOrderRow()
    {
        ConsoleHelper.PrintSubHeader("Lägg till orderrad");

        var orderId = ConsoleHelper.ReadInt("  Order-ID: ");

        if (!_orderService.OrderExists(orderId))
        {
            ConsoleHelper.PrintError("Ordern finns inte.");
            ConsoleHelper.Pause();
            return;
        }

        AddRowToOrder(orderId);
        ConsoleHelper.Pause();
    }

    private void AddRowToOrder(int orderId)
    {
        // Visa tillgängliga produkter
        var products = _productService.GetActive();
        if (products.Count == 0)
        {
            ConsoleHelper.PrintError("Inga aktiva produkter tillgängliga.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"  {"ID",-5} {"Produkt",-20} {"Pris",10} {"Lager",7}");
        Console.WriteLine("  " + new string('-', 45));
        foreach (var p in products)
        {
            Console.WriteLine($"  {p.ProductId,-5} {p.Name,-20} {p.UnitPrice,10:N2} {p.StockQuantity,7}");
        }

        var productId = ConsoleHelper.ReadInt("  Produkt-ID: ");
        var product = _productService.GetById(productId);

        if (product == null || product.Status != "Active")
        {
            ConsoleHelper.PrintError("Ogiltig eller inaktiv produkt.");
            return;
        }

        var quantity = ConsoleHelper.ReadPositiveInt("  Antal: ");

        if (quantity > product.StockQuantity)
        {
            ConsoleHelper.PrintError($"Otillräckligt lager. Tillgängligt: {product.StockQuantity}");
            return;
        }

        _orderService.AddOrderRow(orderId, productId, quantity);
        ConsoleHelper.PrintSuccess($"{quantity} x {product.Name} tillagd.");
    }
}
