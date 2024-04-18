using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Pages.PostsTesting {
    public class EditModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public EditModel(LoginProject.Data.ApplicationDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null) {
                return NotFound();
            }
            Post = post;
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ParentPostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(Post).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PostExists(Post.PostId)) {
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
}
