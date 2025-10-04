using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
      
var videos = new List<Video>
{
    new Video("Hiking Through Zion Canyon", "Emily Travels", 540),
    new Video("Trying the New GoPro Hero", "TechDad", 420),
    new Video("Early Morning Trail Run", "Jake Outdoors", 615),
    new Video("Desk Setup for College Students", "Lily Learns", 470)
};


videos[0].AddComment(new Comment("Ben", "That view at the top was unreal!"));
videos[0].AddComment(new Comment("Sophie", "I went there last year—brought back memories."));
videos[0].AddComment(new Comment("Carlos", "Love the drone shots."));

videos[1].AddComment(new Comment("Tina", "I’ve been waiting for someone to test this camera!"));
videos[1].AddComment(new Comment("Mark", "Battery life seems a little short, but quality looks good."));
videos[1].AddComment(new Comment("Hannah", "Appreciate the honest review."));

videos[2].AddComment(new Comment("Liam", "The sunrise lighting looks perfect."));
videos[2].AddComment(new Comment("Ella", "I need those running shoes—what brand?"));
videos[2].AddComment(new Comment("Drew", "Motivating video. Makes me want to get outside."));

videos[3].AddComment(new Comment("Ava", "Finally a setup that doesn’t cost thousands!"));
videos[3].AddComment(new Comment("Noah", "Good cable management tips."));
videos[3].AddComment(new Comment("Grace", "This gave me some ideas for my dorm room."));
videos[3].AddComment(new Comment("Ethan", "Simple but super clean layout."));

            
            foreach (var video in videos)
            {
                Console.WriteLine("────────────────────────────────────────");
                Console.WriteLine($"Title : {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.LengthSeconds} seconds");
                Console.WriteLine($"Comments ({video.GetCommentCount()}):");

                foreach (var c in video.Comments)
                {
                    Console.WriteLine($"  • {c}");
                }

                Console.WriteLine();
            }

            
        }
    }
}
