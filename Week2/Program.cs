using System;

class Program
{
    static void Main()
    {
        var journal = new Journal();
        var prompts = new PromptGenerator();

        while (true)
        {
            PrintMenu();
            Console.Write("Select an option (1-5): ");
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":

                    string prompt = prompts.GetRandomPrompt();
                    Console.WriteLine($"\n{prompt}");
                    Console.Write("> ");
                    string? response = Console.ReadLine() ?? "";
                    var entry = new Entry
                    {
                        Date = DateTime.Now.ToString("yyyy-MM-dd"),
                        Prompt = prompt,
                        Response = response
                    };
                    journal.Add(entry);
                    Console.WriteLine("Entry added.\n");
                    break;

                case "2":

                    journal.DisplayAll();
                    break;

                case "3":

                    Console.Write("Enter filename to save (e.g., journal.txt): ");
                    string? saveName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(saveName))
                        journal.SaveToFile(saveName);
                    else
                        Console.WriteLine("Save cancelled (no filename).");
                    break;

                case "4":

                    Console.Write("Enter filename to load: ");
                    string? loadName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(loadName))
                        journal.LoadFromFile(loadName);
                    else
                        Console.WriteLine("Load cancelled (no filename).");
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Please choose 1–5.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("Journal Menu");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Quit");
    }
}


