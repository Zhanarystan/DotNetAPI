using System;
using System.Collections.Generic;

namespace DotNetAPI.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public ICollection<AppUser> Members { get; set; }
        public ICollection<Message> Messages { get; set; }
        public Message LastMessage { get; set; }
    }
}