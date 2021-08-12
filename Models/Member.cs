using System;

namespace DotNetAPI.Models
{
    public class Member
    {
        public string AppUserId {get;set;}
        public AppUser AppUser { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        
    }
}