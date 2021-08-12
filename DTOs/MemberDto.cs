using System.Collections.Generic;
using DotNetAPI.Models;

namespace DotNetAPI.DTOs
{
    public class MemberDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
    }
}