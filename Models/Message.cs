using System;

namespace DotNetAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string? FilePath { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public AppUser Author { get; set; }
        public Chat Chat { get; set; }
    }
}