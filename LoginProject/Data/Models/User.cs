using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginProject.Models {
    public class User : IdentityUser<Guid> {
        public string? FullName { get; set; } = default!;
        public ICollection<Post>? Posts { get; set; } = default!;
        [NotMapped]
        public int PostsCount { get; set; } = default!;
        [NotMapped]
        public bool IsModerator { get; set; } = default!;
    }
}
