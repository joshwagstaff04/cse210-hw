using System;

namespace YouTubeVideos
{
    
    public class Comment
    {
        public string Author { get; }
        public string Text { get; }

        public Comment(string author, string text)
        {
            Author = author ?? "Unknown";
            Text = text ?? string.Empty;
        }

        public override string ToString()
        {
            return $"{Author}: {Text}";
        }
    }
}
