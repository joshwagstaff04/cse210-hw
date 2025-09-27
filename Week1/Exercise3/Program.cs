using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Guess My Number ===");

        do
        {
           
            int magic = new Random().Next(1, 101);

            int guesses = 0;
            int guess;

            
            while (true)
            {
                Console.Write("What is your guess? ");

                
                if (!int.TryParse(Console.ReadLine(), out guess))
                {
                    Console.WriteLine("Please enter a whole number.");
                    continue;
                }

                guesses++;

                if (guess < magic)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magic)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"Guesses taken: {guesses}");
                    break;
                }
            }

        } while (WantsToPlayAgain());

        Console.WriteLine("Thanks for playing!");
    }

    static bool WantsToPlayAgain()
    {
        while (true)
        {
            Console.Write("Play again? (yes/no): ");
            string? resp = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (resp == "y" || resp == "yes") return true;
            if (resp == "n" || resp == "no") return false;

            Console.WriteLine("Please answer yes or no.");
        }
    }
}
