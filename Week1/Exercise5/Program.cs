using System;

class Program
{
    static void Main()
    {
        DisplayWelcome();

        string name = PromptUserName();
        int favorite = PromptUserNumber();
        int squared = SquareNumber(favorite);

        DisplayResult(name, squared);
    }

   
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string? name = Console.ReadLine();
        return name ?? string.Empty;
    }

    
    static int PromptUserNumber()
    {
        while (true)
        {
            Console.Write("Please enter your favorite number: ");
            string? raw = Console.ReadLine();

            if (int.TryParse(raw, out int n))
                return n;

            Console.WriteLine("Please enter a whole number (e.g., 42).");
        }
    }

  
    static int SquareNumber(int n) => n * n;


    static void DisplayResult(string name, int squared)
    {
        Console.WriteLine($"{name}, the square of your number is {squared}");
    }
}
