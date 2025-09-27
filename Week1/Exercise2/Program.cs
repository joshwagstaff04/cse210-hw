using System;

class Program
{
    static void Main()
    {
        Console.Write("What is your grade percentage? ");
        int percent = int.Parse(Console.ReadLine());

        string letter;
        if (percent >= 90)      letter = "A";
        else if (percent >= 80) letter = "B";
        else if (percent >= 70) letter = "C";
        else if (percent >= 60) letter = "D";
        else                    letter = "F";

        Console.WriteLine($"Your grade is {letter}.");

        if (percent >= 70)
            Console.WriteLine("Congratulations! You passed the course.");
        else
            Console.WriteLine("Keep trying—you can do it next time!");
    }
}