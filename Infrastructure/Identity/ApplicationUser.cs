using Microsoft.AspNetCore.Identity;

namespace Restaurant_Management.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
    }
}