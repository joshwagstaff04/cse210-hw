using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        protected string _name;
        protected string _desc;
        protected int _points;

        public virtual bool IsComplete => false;
        public string Name => _name;

        protected Goal(string name, string desc, int points)
        { _name = name; _desc = desc; _points = points; }

        public virtual string GetDetailsString()
        {
            string box = IsComplete ? "[X]" : "[ ]";
            return $"{box} {_name} ({_desc}) — {_points} pts";
        }

        public abstract int RecordEvent();
        public abstract string SaveLine();

        public static Goal LoadFrom(string line)
        {
            var p = line.Split('|');
            string t = p[0], n = p[1], d = p[2];
            int pts = int.Parse(p[3], CultureInfo.InvariantCulture);

            switch (t)
            {
                case "Simple":
                    bool done = bool.Parse(p[4]);
                    return new SimpleGoal(n, d, pts, done);
                case "Eternal":
                    int times = p.Length > 4 ? int.Parse(p[4], CultureInfo.InvariantCulture) : 0;
                    return new EternalGoal(n, d, pts, times);
                case "Checklist":
                    int cur = int.Parse(p[4], CultureInfo.InvariantCulture);
                    int target = int.Parse(p[5], CultureInfo.InvariantCulture);
                    int bonus = int.Parse(p[6], CultureInfo.InvariantCulture);
                    return new ChecklistGoal(n, d, pts, target, bonus, cur);
                default: throw new InvalidDataException("Unknown goal type");
            }
        }
    }

    class SimpleGoal : Goal
    {
        private bool _done;
        public SimpleGoal(string name, string desc, int points, bool done = false)
            : base(name, desc, points) { _done = done; }

        public override bool IsComplete => _done;

        public override int RecordEvent()
        { if (_done) return 0; _done = true; return _points; }

        public override string SaveLine()
            => $"Simple|{_name}|{_desc}|{_points}|{_done}";
    }

    class EternalGoal : Goal
    {
        private int _times;
        public EternalGoal(string name, string desc, int points, int times = 0)
            : base(name, desc, points) { _times = times; }

        public override int RecordEvent()
        { _times++; return _points; }

        public override string GetDetailsString()
            => $"[∞] {_name} ({_desc}) — {_points} pts each time (x{_times})";

        public override string SaveLine()
            => $"Eternal|{_name}|{_desc}|{_points}|{_times}";
    }

    class ChecklistGoal : Goal
    {
        private int _cur, _target, _bonus;
        public ChecklistGoal(string name, string desc, int points, int target, int bonus, int cur = 0)
            : base(name, desc, points) { _target = target; _bonus = bonus; _cur = cur; }

        public override bool IsComplete => _cur >= _target;

        public override int RecordEvent()
        {
            if (IsComplete) return 0;
            _cur++;
            int earned = _points;
            if (IsComplete) earned += _bonus;
            return earned;
        }

        public override string GetDetailsString()
        {
            string box = IsComplete ? "[X]" : "[ ]";
            return $"{box} {_name} ({_desc}) — {_points} pts each, bonus {_bonus} at {_target} — Completed {_cur}/{_target}";
        }

        public override string SaveLine()
            => $"Checklist|{_name}|{_desc}|{_points}|{_cur}|{_target}|{_bonus}";
    }

    class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;

        public void Start()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("=== Eternal Quest ===");
                Console.WriteLine($"Score: {_score}\n");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Save Goals");
                Console.WriteLine("4. Load Goals");
                Console.WriteLine("5. Record Event");
                Console.WriteLine("6. Quit");
                Console.Write("Choose: ");

                switch ((Console.ReadLine() ?? "").Trim())
                {
                    case "1": CreateGoal(); break;
                    case "2": ListGoals(true); break;
                    case "3": Save(); break;
                    case "4": Load(); break;
                    case "5": Record(); break;
                    case "6": run = false; break;
                    default:
                        Console.WriteLine("Invalid. Enter to continue...");
                        Console.ReadLine(); break;
                }
            }
        }

        private void CreateGoal()
        {
            Console.Clear();
            Console.WriteLine("Type:\n1. Simple\n2. Eternal\n3. Checklist");
            Console.Write("Choose: ");
            string t = (Console.ReadLine() ?? "").Trim();

            Console.Write("Name: "); string name = Console.ReadLine() ?? "Unnamed";
            Console.Write("Description: "); string desc = Console.ReadLine() ?? "";
            int pts = ReadInt("Points: ");

            if (t == "1") _goals.Add(new SimpleGoal(name, desc, pts));
            else if (t == "2") _goals.Add(new EternalGoal(name, desc, pts));
            else if (t == "3")
            {
                int target = ReadInt("Times to complete: ");
                int bonus = ReadInt("Bonus when done: ");
                _goals.Add(new ChecklistGoal(name, desc, pts, target, bonus));
            }
            else
            { Console.WriteLine("Unknown type.\nEnter to continue..."); Console.ReadLine(); return; }

            Console.WriteLine("Goal created.\nEnter to continue...");
            Console.ReadLine();
        }

        private void ListGoals(bool wait)
        {
            Console.Clear();
            Console.WriteLine("=== Goals ===");
            if (_goals.Count == 0) Console.WriteLine("(none)");
            else for (int i = 0; i < _goals.Count; i++)
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            if (wait) { Console.WriteLine("\nEnter to continue..."); Console.ReadLine(); }
        }

        private void Record()
        {
            if (_goals.Count == 0) { Console.WriteLine("No goals yet.\nEnter to continue..."); Console.ReadLine(); return; }
            ListGoals(false);
            Console.Write("Which goal number? ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > _goals.Count)
            { Console.WriteLine("Bad selection.\nEnter to continue..."); Console.ReadLine(); return; }

            var g = _goals[n - 1];
            int earned = g.RecordEvent();
            _score += earned;
            Console.WriteLine(earned > 0 ? $"+{earned} pts! Score: {_score}" : "No points (maybe already complete).");
            Console.WriteLine("Enter to continue...");
            Console.ReadLine();
        }

        private void Save()
        {
            Console.Write("Filename (e.g., goals.txt): ");
            string file = Console.ReadLine() ?? "goals.txt";
            using (var sw = new StreamWriter(file))
            {
                sw.WriteLine(_score);
                foreach (var g in _goals) sw.WriteLine(g.SaveLine());
            }
            Console.WriteLine("Saved.\nEnter to continue...");
            Console.ReadLine();
        }

        private void Load()
        {
            Console.Write("Filename (e.g., goals.txt): ");
            string file = Console.ReadLine() ?? "goals.txt";
            if (!File.Exists(file)) { Console.WriteLine("Not found.\nEnter to continue..."); Console.ReadLine(); return; }

            var lines = File.ReadAllLines(file);
            if (lines.Length == 0) { Console.WriteLine("Empty file.\nEnter to continue..."); Console.ReadLine(); return; }

            _goals.Clear();
            _score = int.Parse(lines[0], CultureInfo.InvariantCulture);
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                _goals.Add(Goal.LoadFrom(lines[i]));
            }
            Console.WriteLine("Loaded.\nEnter to continue...");
            Console.ReadLine();
        }

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int n) && n >= 0) return n;
                Console.WriteLine("Enter a non-negative number.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var mgr = new GoalManager();
            mgr.Start();
        }
    }
}
