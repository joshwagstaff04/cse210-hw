using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    
    public class Video
    {
        public string Title { get; }
        public string Author { get; }
        public int LengthSeconds { get; }
        private readonly List<Comment> _comments = new List<Comment>();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title ?? "Untitled";
            Author = author ?? "Unknown";
            LengthSeconds = Math.Max(0, lengthSeconds);
        }

        
        public void AddComment(Comment comment)
        {
            if (comment != null) _comments.Add(comment);
        }

        
        public int GetCommentCount() => _comments.Count;

        
        public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();
    }
}
