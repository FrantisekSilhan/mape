using Microsoft.AspNetCore.Identity;

namespace LoginProject.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? FullName { get; set; } = default!;
    }
}
