// Josh Wagstaff — W03 Scripture Memorizer
// I kept this simple: Reference just knows how to format itself.
// Two constructors: single verse and verse range.

namespace ScriptureMemorizer
{
    public class Reference
    {
        public string Book { get; }
        public int Chapter { get; }
        public int StartVerse { get; }
        public int? EndVerse { get; }

        // Single-verse constructor
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = verse;
            EndVerse = null;
        }

        // Verse range constructor
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public string GetDisplayText()
        {
            return EndVerse.HasValue
                ? $"{Book} {Chapter}:{StartVerse}-{EndVerse.Value}"
                : $"{Book} {Chapter}:{StartVerse}";
        }

        public override string ToString() => GetDisplayText();
    }
}
