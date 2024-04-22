using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginProject.Pages.Posts {
    public class ViewModel : PageModel {
        private readonly Data.ApplicationDbContext _context;

        public ViewModel(Data.ApplicationDbContext context) {
            _context = context;
        }

        public Post Post { get; set; } = default!;
        public List<Post> Replies { get; set; } = default!;
        public List<Post> Thread { get; set; } = default!;
        public bool CanDelete { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            if (id == null)
                return NotFound();

            Post? post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.PostId == id);

            if (post == null)
                return NotFound();

            Post = post;

            Thread = await _context.Posts
                .Include(p => p.Author)
                .Where(p => p.PostId == Post.RootPostId || p.PostId == Post.ParentPostId)
                .Distinct()
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();

            Replies = await _context.Posts.Where(p => p.ParentPostId == Post.PostId).ToListAsync();
            if (Post.RootPostId == null) {
                Post.RepliesCount = await _context.Posts.Where(p => p.RootPostId == Post.PostId).CountAsync();
            } else {
                Post.RepliesCount = Replies.Count;
            }

            List<Guid> ids = Replies.Select(r => r.PostId).ToList();
            ids.AddRange(Thread.Select(p => p.PostId).ToList());

            List<PostCount> groupedRepliesCount = await _context.Posts
               .Where(p => ids.Contains((Guid)p.ParentPostId!)) // ParentPostId will never be null here
               .GroupBy(p => p.ParentPostId)
               .Select(g => new PostCount { PostId = g.Key, Count = g.Count() })
               .ToListAsync();

            /*
            var postIds = Replies.Select(r => r.PostId).ToList();
            var postsWithReplies = await _context.Posts
                .Where(p => postIds.Contains((Guid)p.ParentPostId!))
                .ToListAsync();

            var groupedRepliesCount = postsWithReplies
                .GroupBy(p => p.ParentPostId)
                .Select(g => new { PostId = g.Key, Count = g.Count() })
                .ToList();
            */

            Replies.ForEach(r => {
                r.RepliesCount = groupedRepliesCount.FirstOrDefault(rc => rc.PostId == r.PostId)?.Count ?? 0;
            });
            if (Thread.Count > 0) {
                Thread[0].RepliesCount = await _context.Posts.Where(p => p.RootPostId == Post.RootPostId).CountAsync(); ;
                if (Thread.Count > 1) {
                    Thread[1].RepliesCount = groupedRepliesCount.FirstOrDefault(rc => rc.PostId == Thread[1].PostId)?.Count ?? 0;
                }
            }

            CanDelete = User.IsInRole("admin") || User.IsInRole("moderator") || post.AuthorId == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Page();
        }
        private class PostCount {
            public Guid? PostId { get; set; }
            public int Count { get; set; }
        }
    }
}
