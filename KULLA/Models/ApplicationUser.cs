using Microsoft.AspNetCore.Identity;

namespace KULLA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Ortak { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
