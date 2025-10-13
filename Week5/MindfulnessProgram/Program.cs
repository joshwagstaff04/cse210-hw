// W05 Project: Mindfulness Program
// Author: Josh Wagstaff
//
// This program helps you slow down and practice mindfulness.
// It includes breathing, reflection, listing, and a body scan activity.
// I also added a small CSV log to track sessions just for fun.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

abstract class Activity
{
    public string Name { get; }
    public string Description { get; }
    public int DurationSeconds { get; private set; }

    protected Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Run()
    {
        ShowStart();
        Execute();
        ShowEnd();
    }

    protected void ShowStart()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {Name}.");
        Console.WriteLine();
        Console.WriteLine(Description);
        Console.WriteLine();
        DurationSeconds = AskForDuration();
        Console.WriteLine("Get ready to begin...");
        Spinner(3);
    }

    protected void ShowEnd()
    {
        Console.WriteLine();
        Console.WriteLine("Nice job taking a few mindful moments.");
        Spinner(2);
        Console.WriteLine($"You just finished the {Name} for {DurationSeconds} seconds.");
        Spinner(2);
    }

    protected abstract void Execute();

    // shared helper methods --------------------

    private int AskForDuration()
    {
        while (true)
        {
            Console.Write("How many seconds would you like this session to last? ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int num) && num >= 10 && num <= 3600)
                return num;
            Console.WriteLine("Please enter a number between 10 and 3600.");
        }
    }

    protected static void Spinner(int seconds)
    {
        char[] frames = new[] { '|', '/', '-', '\\' };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write($"\r{frames[i++ % frames.Length]}");
            Thread.Sleep(100);
        }
        Console.Write("\r \r");
    }

    protected static void Countdown(int seconds, string prefix = "")
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write($"\r{prefix}{i}   ");
            Thread.Sleep(1000);
        }
        Console.Write("\r" + new string(' ', (prefix?.Length ?? 0) + 6) + "\r");
    }

    protected static void ProgressBar(int seconds, string label)
    {
        int totalTicks = seconds * 20;
        for (int t = 0; t < totalTicks; t++)
        {
            double norm = (double)t / totalTicks;
            double eased = 1 - Math.Pow(1 - norm, 3);
            int width = 20;
            int fill = (int)Math.Round(eased * width);
            Console.Write("\r" + label + " [" + new string('#', fill) + new string('-', width - fill) + "]");
            Thread.Sleep(50);
        }
        Console.Write("\r" + new string(' ', 40) + "\r");
    }
}

// ---------------- Breathing ----------------
class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity helps you relax by breathing in and out slowly. Try to clear your mind and just focus on the breath.")
    { }

    protected override void Execute()
    {
        int elapsed = 0;
        int inhale = 4;
        int exhale = 4;

        while (elapsed < DurationSeconds)
        {
            Console.WriteLine("Breathe in...");
            ProgressBar(Math.Min(inhale, DurationSeconds - elapsed), "In ");
            elapsed += inhale;

            if (elapsed < DurationSeconds)
            {
                Console.WriteLine("Breathe out...");
                ProgressBar(Math.Min(exhale, DurationSeconds - elapsed), "Out");
                elapsed += exhale;
            }
        }

        SessionLog.Write(Name, DurationSeconds, "4s in / 4s out pattern");
    }
}

// ---------------- Reflection ----------------
class ReflectionActivity : Activity
{
    private static readonly string[] Prompts =
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really hard.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly string[] Questions =
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different?",
        "What’s your favorite part of this experience?",
        "What can you learn from it for other situations?",
        "What did you learn about yourself?",
        "How can you remember this feeling in the future?"
    };

    public ReflectionActivity() : base(
        "Reflection Activity",
        "Think about a time when you showed strength or resilience. Let the prompts help you explore it deeper.")
    { }

    protected override void Execute()
    {
        Random rnd = new Random();
        string prompt = Prompts[rnd.Next(Prompts.Length)];

        Console.WriteLine();
        Console.WriteLine("Consider this prompt:");
        Console.WriteLine($" → {prompt}");
        Console.WriteLine("When you have something in mind, we’ll start reflecting...");
        Countdown(5, "Starting in ");

        Queue<string> questions = new Queue<string>(Questions.OrderBy(_ => rnd.Next()));

        int elapsed = 0;
        while (elapsed < DurationSeconds)
        {
            if (questions.Count == 0)
                questions = new Queue<string>(Questions.OrderBy(_ => rnd.Next()));

            string q = questions.Dequeue();
            Console.WriteLine();
            Console.WriteLine(q);
            Spinner(5);
            elapsed += 5;
        }

        SessionLog.Write(Name, DurationSeconds, $"Prompt: {prompt}");
    }
}

// ---------------- Listing ----------------
class ListingActivity : Activity
{
    private static readonly string[] Prompts =
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you’ve helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your heroes?"
    };

    public ListingActivity() : base(
        "Listing Activity",
        "You’ll list as many positive things as you can about a certain topic.")
    { }

    protected override void Execute()
    {
        Random rnd = new Random();
        string prompt = Prompts[rnd.Next(Prompts.Length)];
        Console.WriteLine();
        Console.WriteLine("Consider this prompt:");
        Console.WriteLine($" → {prompt}");
        Console.WriteLine("Get ready to start listing items...");
        Countdown(5, "Starting in ");

        Console.WriteLine("Begin! (Press Enter after each item.)");
        int count = 0;
        DateTime end = DateTime.Now.AddSeconds(DurationSeconds);

        while (DateTime.Now < end)
        {
            Console.Write("> ");
            string? item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item)) count++;
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {count} item(s).");
        Spinner(2);

        SessionLog.Write(Name, DurationSeconds, $"Prompt: {prompt}; Count={count}");
    }
}

// ---------------- Body Scan ----------------
class BodyScanActivity : Activity
{
    private static readonly string[] Regions =
    {
        "Top of the head", "Forehead and eyes", "Jaw and neck", "Shoulders",
        "Arms and hands", "Chest", "Stomach", "Hips", "Thighs", "Calves", "Feet"
    };

    public BodyScanActivity() : base(
        "Body Scan Activity",
        "This one guides your focus slowly through parts of the body. Just notice sensations without judging them.")
    { }

    protected override void Execute()
    {
        int perRegion = Math.Max(2, DurationSeconds / Regions.Length);
        int elapsed = 0;
        int i = 0;

        while (elapsed < DurationSeconds)
        {
            string region = Regions[i % Regions.Length];
            Console.WriteLine($"Focus on: {region}");
            ProgressBar(Math.Min(perRegion, DurationSeconds - elapsed), "Scan");
            elapsed += perRegion;
            i++;
        }

        SessionLog.Write(Name, DurationSeconds, "Body scan cycle complete");
    }
}

// ---------------- Session Log ----------------
static class SessionLog
{
    private const string FileName = "mindfulness_log.csv";

    public static void Write(string activity, int seconds, string details = "")
    {
        try
        {
            bool header = !File.Exists(FileName);
            using StreamWriter sw = new StreamWriter(FileName, append: true);
            if (header)
            {
                sw.WriteLine("date,activity,duration,details");
            }
            sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{activity},{seconds},{details.Replace(',', ';')}");
        }
        catch
        {
            // Not a big deal if it fails
        }
    }
}

// ---------------- Program (menu) ----------------
class Program
{
    static void Main()
    {
        // The hardest part for me was getting the timing for the spinner and progress bar to feel smooth.

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("-------------------");
            Console.WriteLine("1) Breathing Activity");
            Console.WriteLine("2) Reflection Activity");
            Console.WriteLine("3) Listing Activity");
            Console.WriteLine("4) Body Scan Activity");
            Console.WriteLine("5) Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            Activity? activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => new BodyScanActivity(),
                "5" => null,
                _ => null
            };

            if (choice == "5")
            {
                Console.WriteLine("Goodbye! Hope you feel a bit more relaxed.");
                Spinner(2);
                break;
            }

            if (activity == null)
            {
                Console.WriteLine("Please enter a number 1–5.");
                Thread.Sleep(1000);
                continue;
            }

            activity.Run();
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }
    }

    private static void Spinner(int seconds)
    {
        char[] frames = new[] { '|', '/', '-', '\\' };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write($"\r{frames[i++ % frames.Length]}");
            Thread.Sleep(100);
        }
        Console.Write("\r \r");
    }
}
