using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LoginProject.Pages.Users {
    public class SearchModel : PageModel {
        private readonly Data.ApplicationDbContext _context;
        public SearchModel(Data.ApplicationDbContext context) {
            _context = context;
        }
        [BindProperty]
        public InputIM Input { get; set; } = default!;
        public List<User> Users { get; set; } = default!;
        public void OnGet() {
        }
        public async Task<IActionResult> OnPostAsync() {
            if (string.IsNullOrEmpty(Input.UserName)) {
                return RedirectToPage("./Index");
            }

            Users = await _context.Users
                .Where(s => s.UserName!.ToLower().Contains(Input.UserName.ToLower()))
                .Select(s => new User {
                    Id = s.Id,
                    UserName = s.UserName,
                    PostsCount = s.Posts == null ? 0 : s.Posts.Count(),
                })
                .Take(20)
                .ToListAsync();

            return Page();
        }
    }
    public class InputIM {
        [Required]
        public string UserName { get; set; } = default!;
    }
}
