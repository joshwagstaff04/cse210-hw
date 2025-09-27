using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private readonly List<string> _prompts = new()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        
        "What is one small win I had today?",
        "What’s something I learned today (about myself or the world)?"
    };

    private readonly Random _rng = new();

    public string GetRandomPrompt()
    {
        int i = _rng.Next(0, _prompts.Count);
        return _prompts[i];
    }
}
