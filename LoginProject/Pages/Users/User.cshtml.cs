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

            Posts = await _context.Posts.Where(p => p.AuthorId == UserProfile.Id).ToListAsync();


            return Page();
        }
    }
}
