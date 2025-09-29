// Josh Wagstaff — W03 Scripture Memorizer
// Scripture ties it together: holds the Reference, splits text into Word objects,
// and controls hiding.

using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptureMemorizer
{
    public class Scripture
    {
        private readonly Reference _reference;
        private readonly List<Word> _words;
        private readonly Random _rng = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = new List<Word>();

            // Split by whitespace so I can track each word separately.
            foreach (var token in text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries))
            {
                _words.Add(new Word(token));
            }
        }

        public string GetDisplayText()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_reference.GetDisplayText());
            for (int i = 0; i < _words.Count; i++)
            {
                if (i > 0) sb.Append(' ');
                sb.Append(_words[i].GetDisplayText());
            }
            return sb.ToString();
        }

       
        public void HideRandomWords(int count, bool chooseOnlyVisible = true)
        {
            if (count <= 0) return;

            if (chooseOnlyVisible)
            {
                var candidates = new List<int>();
                for (int i = 0; i < _words.Count; i++)
                    if (!_words[i].IsHidden) candidates.Add(i);

                if (candidates.Count == 0) return;

                for (int i = 0; i < count && candidates.Count > 0; i++)
                {
                    int pick = _rng.Next(candidates.Count);
                    int idx = candidates[pick];
                    _words[idx].Hide();
                    candidates.RemoveAt(pick);
                }
            }
            else
            {
                // Core behavior from the spec: random pick can hit hidden words again.
                for (int i = 0; i < count; i++)
                {
                    int idx = _rng.Next(_words.Count);
                    _words[idx].Hide();
                }
            }
        }

        public bool AllHidden()
        {
            foreach (var w in _words)
                if (!w.IsHidden) return false;
            return true;
        }
    }
}
