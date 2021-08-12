using System;
using System.Collections.Generic;
using DotNetAPI.DTOs;

namespace DotNetAPI.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public ICollection<AppUser> Members { get; set; } = new List<AppUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public Message? LastMessage { get; set; }
    }
}