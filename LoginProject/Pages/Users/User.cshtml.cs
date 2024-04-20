using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Pages.Users {
    public class UserModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _users;

        public UserModel(LoginProject.Data.ApplicationDbContext context, UserManager<User> users) {
            _context = context;
            _users = users;
        }

        public User? UserProfile { get; set; } = default!;
        public List<Post> Posts { get; set; } = default!;

        public async Task<IActionResult> OnGet(string username) {
            UserProfile = await _users.FindByNameAsync(username);

            if (UserProfile == null) {
                return NotFound();
            }

            Posts = await _context.Posts.Where(p => p.AuthorId == UserProfile.Id).OrderByDescending(p => p.CreatedAt).ToListAsync();

            var groupedRepliesCount = await _context.Posts
                .Where(p => Posts.Select(p => p.PostId).ToList().Contains((Guid)p.ParentPostId!))
                .GroupBy(p => p.ParentPostId)
                .Select(g => new PostCount { PostId = g.Key, Count = g.Count() })
                .ToListAsync();

            Posts.ForEach(p => {
                p.RepliesCount = groupedRepliesCount.FirstOrDefault(rc => rc.PostId == p.PostId)?.Count ?? 0;
            });

            return Page();
        }
        private class PostCount {
            public Guid? PostId { get; set; }
            public int Count { get; set; }
        }
    }
}
