using System;
using System.Collections.Generic;
using DotNetAPI.Models;

namespace DotNetAPI.DTOs
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public MemberDto Destination { get; set; }
        public ICollection<MemberDto> Members  { get; set; }
        public Message? LastMessage { get; set; }
    }
}