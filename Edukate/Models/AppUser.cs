using Microsoft.AspNetCore.Identity;

namespace Edukate.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = null!;
    }
}
