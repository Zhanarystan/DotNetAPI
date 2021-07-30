using Microsoft.AspNetCore.Identity;

namespace DotNetAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }
    }
}