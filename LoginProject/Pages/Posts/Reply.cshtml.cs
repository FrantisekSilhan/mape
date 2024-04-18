using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginProject.Pages.Posts {
    public class ReplyModel : PageModel {
        private readonly Data.ApplicationDbContext _context;

        public ReplyModel(Data.ApplicationDbContext context) {
            _context = context;
        }

        public Post Post { get; set; } = default!;
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
            if (Post.RootPostId == null)
                Post.RepliesCount = await _context.Posts.Where(p => p.RootPostId == Post.PostId).CountAsync();
            else
                Post.RepliesCount = await _context.Posts.Where(p => p.ParentPostId == Post.PostId).CountAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id) {
            if (!ModelState.IsValid)
                return Page();

            if (id == null)
                return NotFound();

            Post? post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
                return NotFound();

            Guid rootPostId;
            if (post.RootPostId != null)
                rootPostId = (Guid)post.RootPostId;
            else
                rootPostId = post.PostId;

            Post reply = new Post {
                PostId = Guid.NewGuid(),
                AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                Content = Input.Content,
                CreatedAt = DateTime.UtcNow,
                ParentPostId = post.PostId,
                RootPostId = rootPostId,
            };

            await _context.Posts.AddAsync(reply);
            await _context.SaveChangesAsync();

            return RedirectToPage("./View", new { id = post.PostId });
        }
        public class PostIM {
            public string Content { get; set; } = default!;
        }
    }
}
