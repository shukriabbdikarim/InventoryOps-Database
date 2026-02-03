using InventoryOps.ConsoleApp.Data;
using InventoryOps.ConsoleApp.Helpers;
using InventoryOps.ConsoleApp.Menus;
using InventoryOps.ConsoleApp.Services;

// Anslutningssträng – ändra vid behov
const string connectionString =
    @"Server=PAPPAS\SQLEXPRESS;Database=InventoryOps;Trusted_Connection=True;TrustServerCertificate=True;";

Console.OutputEncoding = System.Text.Encoding.UTF8;

using var context = new InventoryOpsContext(connectionString);

// Testa anslutningen
try
{
    if (!context.Database.CanConnect())
    {
        ConsoleHelper.PrintError("Kunde inte ansluta till databasen.");
        ConsoleHelper.PrintInfo("Kontrollera att SQL Server körs och att databasen 'InventoryOps' finns.");
        ConsoleHelper.PrintInfo($"Anslutningssträng: {connectionString}");
        return;
    }
}
catch (Exception ex)
{
    ConsoleHelper.PrintError($"Databasfel: {ex.Message}");
    return;
}

// Services
var customerService = new CustomerService(context);
var productService = new ProductService(context);
var orderService = new OrderService(context);
var reportService = new ReportService(context);

// Menyer
var customerMenu = new CustomerMenu(customerService);
var productMenu = new ProductMenu(productService);
var orderMenu = new OrderMenu(orderService, productService);
var reportMenu = new ReportMenu(reportService);

var mainMenu = new MainMenu(customerMenu, productMenu, orderMenu, reportMenu);
mainMenu.Show();

Console.WriteLine("Hej då!");
