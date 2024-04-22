using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LoginProject.Pages.Posts {
    public class ReplyModel : PageModel {
        private readonly Data.ApplicationDbContext _context;

        public ReplyModel(Data.ApplicationDbContext context) {
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id) {
            if (!ModelState.IsValid) {
                if (id == null)
                    return NotFound();

                Post? post2 = await _context.Posts
                    .Include(p => p.Author)
                    .FirstOrDefaultAsync(p => p.PostId == id);

                if (post2 == null)
                    return NotFound();

                Post = post2;
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

                return Page();
            }

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
            [Required(ErrorMessage = "Message is required.")]
            [MaxLength(4096, ErrorMessage = "Message cannot be longer than 4096 characters.")]
            public string Content { get; set; } = default!;
        }
    }
}
