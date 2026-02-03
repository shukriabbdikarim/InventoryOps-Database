using InventoryOps.ConsoleApp.Helpers;
using InventoryOps.ConsoleApp.Services;

namespace InventoryOps.ConsoleApp.Menus;

public class CustomerMenu
{
    private readonly CustomerService _service;

    public CustomerMenu(CustomerService service)
    {
        _service = service;
    }

    public void Show()
    {
        while (true)
        {
            ConsoleHelper.PrintHeader("Kunder");
            Console.WriteLine("  1. Lista alla kunder");
            Console.WriteLine("  2. Skapa ny kund");
            Console.WriteLine("  3. Uppdatera kund");
            Console.WriteLine("  4. Ta bort kund");
            Console.WriteLine("  0. Tillbaka");

            switch (ConsoleHelper.ReadMenuChoice())
            {
                case 1: ListAll(); break;
                case 2: Create(); break;
                case 3: Update(); break;
                case 4: Delete(); break;
                case 0: return;
                default:
                    ConsoleHelper.PrintError("Ogiltigt val.");
                    break;
            }
        }
    }

    private void ListAll()
    {
        ConsoleHelper.PrintSubHeader("Alla kunder");
        var customers = _service.GetAll();

        if (customers.Count == 0)
        {
            ConsoleHelper.PrintInfo("Inga kunder hittades.");
        }
        else
        {
            Console.WriteLine($"  {"ID",-5} {"Namn",-25} {"E-post",-30}");
            Console.WriteLine("  " + new string('-', 60));
            foreach (var c in customers)
            {
                Console.WriteLine($"  {c.CustomerId,-5} {c.Name,-25} {c.Email,-30}");
            }
        }

        ConsoleHelper.Pause();
    }

    private void Create()
    {
        ConsoleHelper.PrintSubHeader("Skapa ny kund");

        var name = ConsoleHelper.ReadNonEmpty("  Namn: ");
        var email = ConsoleHelper.ReadEmail("  E-post: ");

        if (_service.EmailExists(email))
        {
            ConsoleHelper.PrintError("E-postadressen finns redan.");
            ConsoleHelper.Pause();
            return;
        }

        _service.Create(name, email);
        ConsoleHelper.PrintSuccess("Kund skapad.");
        ConsoleHelper.Pause();
    }

    private void Update()
    {
        ConsoleHelper.PrintSubHeader("Uppdatera kund");

        var id = ConsoleHelper.ReadInt("  Kund-ID: ");
        var customer = _service.GetById(id);

        if (customer == null)
        {
            ConsoleHelper.PrintError("Kunden hittades inte.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine($"  Nuvarande: {customer.Name} ({customer.Email})");
        var name = ConsoleHelper.ReadNonEmpty("  Nytt namn: ");
        var email = ConsoleHelper.ReadEmail("  Ny e-post: ");

        if (_service.EmailExists(email, id))
        {
            ConsoleHelper.PrintError("E-postadressen används redan av en annan kund.");
            ConsoleHelper.Pause();
            return;
        }

        _service.Update(id, name, email);
        ConsoleHelper.PrintSuccess("Kund uppdaterad.");
        ConsoleHelper.Pause();
    }

    private void Delete()
    {
        ConsoleHelper.PrintSubHeader("Ta bort kund");

        var id = ConsoleHelper.ReadInt("  Kund-ID: ");
        var customer = _service.GetById(id);

        if (customer == null)
        {
            ConsoleHelper.PrintError("Kunden hittades inte.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine($"  Kund: {customer.Name} ({customer.Email})");
        if (customer.Orders.Count > 0)
        {
            Console.WriteLine($"  OBS: Kunden har {customer.Orders.Count} order(s) som också tas bort.");
        }

        if (!ConsoleHelper.Confirm("  Vill du ta bort kunden?"))
        {
            ConsoleHelper.PrintInfo("Avbrutet.");
            ConsoleHelper.Pause();
            return;
        }

        _service.Delete(id);
        ConsoleHelper.PrintSuccess("Kund borttagen.");
        ConsoleHelper.Pause();
    }
}
