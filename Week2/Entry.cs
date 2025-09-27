using System;

public class Entry
{
    public string Date { get; set; } = "";
    public string Prompt { get; set; } = "";
    public string Response { get; set; } = "";

    public override string ToString()
        => $"[{Date}]\nPrompt : {Prompt}\nEntry  : {Response}\n";

    
    private const string Sep = "~|~";

    public string Serialize()
        => string.Join(Sep, new[] { Date, Prompt, Response });

    public static Entry Deserialize(string line)
    {
        const string Sep = "~|~";
        var parts = line.Split(Sep);
        
        var e = new Entry();
        if (parts.Length > 0) e.Date = parts[0];
        if (parts.Length > 1) e.Prompt = parts[1];
        if (parts.Length > 2) e.Response = parts[2];
        return e;
    }
}
