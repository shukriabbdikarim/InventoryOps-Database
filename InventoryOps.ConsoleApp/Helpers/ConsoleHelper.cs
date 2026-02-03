namespace InventoryOps.ConsoleApp.Helpers;

public static class ConsoleHelper
{
    public static void PrintHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 40));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('=', 40));
    }

    public static void PrintSubHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {title} ---");
    }

    public static int ReadMenuChoice(string prompt = "Val: ")
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine()?.Trim(), out int choice))
            return choice;
        return -1;
    }

    public static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input))
                return input;
            Console.WriteLine("  Fältet får inte vara tomt.");
        }
    }

    public static string ReadEmail(string prompt)
    {
        while (true)
        {
            var input = ReadNonEmpty(prompt);
            if (input.Contains('@') && input.Contains('.'))
                return input;
            Console.WriteLine("  Ogiltig e-postadress. Måste innehålla @ och punkt.");
        }
    }

    public static int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value) && value > 0)
                return value;
            Console.WriteLine("  Ange ett positivt heltal.");
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value))
                return value;
            Console.WriteLine("  Ange ett giltigt heltal.");
        }
    }

    public static bool Confirm(string prompt)
    {
        Console.Write($"{prompt} (j/n): ");
        var input = Console.ReadLine()?.Trim().ToLower();
        return input == "j" || input == "ja";
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Tryck Enter för att fortsätta...");
        Console.ReadLine();
    }

    public static void PrintSuccess(string message)
    {
        Console.WriteLine($"  [OK] {message}");
    }

    public static void PrintError(string message)
    {
        Console.WriteLine($"  [FEL] {message}");
    }

    public static void PrintInfo(string message)
    {
        Console.WriteLine($"  {message}");
    }
}
