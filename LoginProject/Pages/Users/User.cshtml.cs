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

        public User UserProfile { get; set; } = default!;
        public List<Post> Posts { get; set; } = default!;
        public bool CanEdit { get; set; } = default!;
        private int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = default!;
        public int TotalPages { get; set; } = default!;

        public async Task<IActionResult> OnGet(string username, int peji = 1) {
            var userProfile = await _users.FindByNameAsync(username);

            if (userProfile == null) {
                return NotFound();
            }

            UserProfile = userProfile;

            UserProfile.IsModerator = await _users.IsInRoleAsync(UserProfile, "moderator");
            CanEdit = User.IsInRole("admin") && !await _users.IsInRoleAsync(UserProfile, "admin");

            Posts = await _context.Posts
                .Where(p => p.AuthorId == UserProfile.Id)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((peji - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var groupedRepliesCount = await _context.Posts
                .Where(p => Posts.Select(p => p.PostId).ToList().Contains((Guid)p.ParentPostId!))
                .GroupBy(p => p.ParentPostId)
                .Select(g => new PostCount { PostId = g.Key, Count = g.Count() })
                .ToListAsync();

            Posts.ForEach(p => {
                p.RepliesCount = groupedRepliesCount.FirstOrDefault(rc => rc.PostId == p.PostId)?.Count ?? 0;
            });

            CurrentPage = peji;
            TotalPages = (int)Math.Ceiling(await _context.Posts.Where(p => p.AuthorId == UserProfile.Id).CountAsync() / (double)PageSize);

            return Page();
        }
        private class PostCount {
            public Guid? PostId { get; set; }
            public int Count { get; set; }
        }
    }
}
