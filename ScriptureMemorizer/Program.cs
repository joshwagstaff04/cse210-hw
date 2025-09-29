// Josh Wagstaff — W03 Scripture Memorizer
// Exceeding core (optional toggle):
// I experimented with two modes: core (may re-pick hidden words) and a stricter mode
// that only hides visible words. For this submission I left it on the core setting.

using System;
using System.Text;

namespace ScriptureMemorizer
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            // I picked one scripture so I can focus on memorizing it.
            // (Multi-verse reference supported by Reference range constructor.)
            Scripture scripture = new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the LORD with all thine heart; and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."
            );

            // Slower pace feels more manageable for practice.
            const int HideCount = 2;

            // Core behavior per spec: may re-pick already-hidden words.
            bool chooseOnlyVisible = false;

            Console.Clear();
            Display(scripture);

            while (true)
            {
                Console.WriteLine();
                Console.Write("Press ENTER to hide words, or type 'quit' to exit: ");
                string input = Console.ReadLine()?.Trim().ToLowerInvariant() ?? string.Empty;

                if (input == "quit") break;

                scripture.HideRandomWords(HideCount, chooseOnlyVisible);

                Console.Clear();
                Display(scripture);

                if (scripture.AllHidden())
                {
                    Console.WriteLine();
                    Console.WriteLine("All words hidden. Nice job!");
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Thanks for practicing!");
        }

        static void Display(Scripture s)
        {
            Console.WriteLine(s.GetDisplayText());
        }
    }
}
