using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private readonly List<Entry> _entries = new();

    public void Add(Entry entry) => _entries.Add(entry);

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        Console.WriteLine("\n--- Journal ---");
        foreach (var e in _entries)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void SaveToFile(string filename)
    {
        using var writer = new StreamWriter(filename);
        foreach (var e in _entries)
        {
            writer.WriteLine(e.Serialize());
        }
        Console.WriteLine($"Saved {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} to \"{filename}\".");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File not found: {filename}");
            return;
        }

        var loaded = new List<Entry>();
        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            loaded.Add(Entry.Deserialize(line));
        }

        _entries.Clear();
        _entries.AddRange(loaded);
        Console.WriteLine($"Loaded {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} from \"{filename}\".");
    }
}
