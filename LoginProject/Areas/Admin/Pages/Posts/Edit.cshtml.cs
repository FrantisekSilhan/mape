using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LoginProject.Areas.Admin.Pages.Posts {
    public class EditModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public EditModel(LoginProject.Data.ApplicationDbContext context) {
            _context = context;
        }

        [BindProperty]
        public PostIM Input { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null) {
                return NotFound();
            }
            Input = new PostIM {
                PostId = post.PostId,
                AuthorId = post.AuthorId,
                Content = post.Content,
            };
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ParentPostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            ViewData["RootPostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.PostId == Input.PostId);
            if (post == null) {
                return NotFound();
            }

            post.AuthorId = Input.AuthorId;
            post.Content = Input.Content;

            _context.Attach(post).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PostExists(post.PostId)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostExists(Guid id) {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }

    public class PostIM {
        public Guid PostId { get; set; }
        [Required]
        public Guid AuthorId { get; set; } = default!;
        [Required]
        public string Content { get; set; } = default!;
    }
}
