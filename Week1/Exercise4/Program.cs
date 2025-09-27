using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        List<int> numbers = new List<int>();

        
        while (true)
        {
            Console.Write("Enter number: ");
            string? raw = Console.ReadLine();

            if (!int.TryParse(raw, out int n))
            {
                Console.WriteLine("Please enter a whole number.");
                continue;
            }

            if (n == 0) break; 
            numbers.Add(n);
        }

        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers entered.");
            return;
        }

        int sum = 0;
        int max = numbers[0];
        foreach (int x in numbers)
        {
            sum += x;
            if (x > max) max = x;
        }
        double average = (double)sum / numbers.Count;

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        int? smallestPositive = null;
        foreach (int x in numbers)
        {
            if (x > 0 && (smallestPositive == null || x < smallestPositive))
            {
                smallestPositive = x;
            }
        }
        if (smallestPositive != null)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers were entered.");
        }

        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int x in numbers)
        {
            Console.WriteLine(x);
        }
    }
}
