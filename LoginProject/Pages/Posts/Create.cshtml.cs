using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoginProject.Data;
using LoginProject.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Pages.Posts {
    public class CreateModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _users;

        public CreateModel(LoginProject.Data.ApplicationDbContext context, UserManager<User> users) {
            _context = context;
            _users = users;
        }

        public IActionResult OnGet() {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ParentPostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            return Page();
        }

        [BindProperty]
        public PostIM Post { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Guid? parentPostId, Guid? rootPostId) {
            if (!ModelState.IsValid) {
                return Page();
            }

            if (Post == null) {
                return NotFound();
            }

            var post = new Post {
                PostId = Guid.NewGuid(),
                Author = (await _users.GetUserAsync(User))!,
                Content = Post.Content,
                CreatedAt = DateTime.UtcNow,
                ParentPostId = parentPostId,
                RootPostId = rootPostId,
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public class PostIM {
            public string Content { get; set; } = default!;
        }
    }
}