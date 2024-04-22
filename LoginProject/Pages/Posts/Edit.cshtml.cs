using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LoginProject.Pages.Posts {
    public class EditModel : PageModel {
        private readonly Data.ApplicationDbContext _context;

        public EditModel(Data.ApplicationDbContext context) {
            _context = context;
        }
        public Post Post { get; set; } = default!;
        public List<Post> Thread { get; set; } = default!;
        [BindProperty]
        public PostIM Input { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            if (id == null)
                return NotFound();

            Post? post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
                return NotFound();

            Post = post;

            if (Post.AuthorId != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
                return Forbid();

            Thread = await _context.Posts
                .Include(p => p.Author)
                .Where(p => p.PostId == Post.RootPostId || p.PostId == Post.ParentPostId)
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();

            if (Post.RootPostId == null)
                Post.RepliesCount = await _context.Posts.Where(p => p.RootPostId == Post.PostId).CountAsync();
            else
                Post.RepliesCount = await _context.Posts.Where(p => p.ParentPostId == Post.PostId).CountAsync();

            if (Thread.Count > 0) {
                Thread[0].RepliesCount = await _context.Posts.Where(p => p.RootPostId == Post.RootPostId).CountAsync();
                if (Thread.Count > 1)
                    Thread[1].RepliesCount = await _context.Posts.Where(p => p.ParentPostId == Thread[1].PostId).CountAsync();
            }

            Input = new PostIM {
                Content = Post.Content
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id) {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid) {
                return RedirectToPage("./View", new { id = id.Value });
            }

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null)
                return NotFound();

            Post = post;

            if (Post.AuthorId != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
                return Forbid();

            Post.Content = Input.Content;
            Post.EditedAt = DateTime.UtcNow;

            _context.Posts.Attach(Post).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PostExists(Post.PostId))
                    return NotFound();
                else
                    throw;
            }


            return RedirectToPage("./View", new { id = id.Value });
        }
        private bool PostExists(Guid id) {
            return _context.Posts.Any(e => e.PostId == id);
        }

        public class PostIM {
            [Required(ErrorMessage = "Message is required.")]
            [MaxLength(4096, ErrorMessage = "Message cannot be longer than 4096 characters.")]
            public string Content { get; set; } = default!;
        }
    }
}
