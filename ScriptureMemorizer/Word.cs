// Josh Wagstaff — W03 Scripture Memorizer
// Word owns hiding/display. I only hide letters so punctuation stays.

using System.Text;

namespace ScriptureMemorizer
{
    public class Word
    {
        private readonly string _text;
        private bool _hidden;

        public Word(string text)
        {
            _text = text;
            _hidden = false;
        }

        // If a token has no letters (like “;” or “—”), I treat it as always visible.
        public bool IsHidden => _hidden || !HasLetters(_text);

        public void Hide()
        {
            if (HasLetters(_text))
            {
                _hidden = true;
            }
        }

        public string GetDisplayText()
        {
            if (!IsHidden) return _text;

            // I only replace letters so punctuation stays. Helps me see the sentence shape.
            var sb = new StringBuilder(_text.Length);
            foreach (char c in _text)
            {
                sb.Append(char.IsLetter(c) ? '_' : c);
            }
            return sb.ToString();
        }

        private static bool HasLetters(string s)
        {
            foreach (char c in s)
                if (char.IsLetter(c)) return true;
            return false;
        }
    }
}
