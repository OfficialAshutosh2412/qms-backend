using Microsoft.AspNetCore.Identity;


namespace QualityWebSystem.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
