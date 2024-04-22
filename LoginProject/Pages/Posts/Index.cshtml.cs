using LoginProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LoginProject.Pages.Posts {
    public class IndexModel : PageModel {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context) {
            _context = context;
        }

        public List<Post> Posts { get; set; } = default!;
        [BindProperty]
        public PostIM Input { get; set; } = default!;
        private int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = default!;
        public int TotalPages { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int peji = 1) {
            Posts = await _context.Posts
                .Include(p => p.Author)
                .Where(p => p.RootPostId == null)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((peji - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var groupedRepliesCount = await _context.Posts
                .Where(p => Posts.Select(p => p.PostId).ToList().Contains((Guid)p.RootPostId!))
                .GroupBy(p => p.RootPostId)
                .Select(g => new PostCount { PostId = g.Key, Count = g.Count() })
                .ToListAsync();

            Posts.ForEach(p => {
                p.RepliesCount = groupedRepliesCount.FirstOrDefault(rc => rc.PostId == p.PostId)?.Count ?? 0;
            });

            CurrentPage = peji;
            TotalPages = (int)Math.Ceiling(await _context.Posts.Where(p => p.RootPostId == null).CountAsync() / (double)PageSize);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int peji = 1) {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            if (!ModelState.IsValid) {
                Posts = await _context.Posts
                    .Include(p => p.Author)
                    .Where(p => p.RootPostId == null)
                    .Skip((peji - 1) * PageSize)
                    .Take(PageSize)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();

                var groupedRepliesCount = await _context.Posts
                    .Where(p => Posts.Select(p => p.PostId).ToList().Contains((Guid)p.RootPostId!))
                    .GroupBy(p => p.RootPostId)
                    .Select(g => new PostCount { PostId = g.Key, Count = g.Count() })
                    .ToListAsync();

                Posts.ForEach(p => {
                    p.RepliesCount = groupedRepliesCount.FirstOrDefault(rc => rc.PostId == p.PostId)?.Count ?? 0;
                });

                CurrentPage = peji;
                TotalPages = (int)Math.Ceiling(await _context.Posts.Where(p => p.RootPostId == null).CountAsync() / (double)PageSize);

                return Page();
            }

            Post post = new Post {
                PostId = Guid.NewGuid(),
                AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                Content = Input.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./View", new { id = post.PostId });
        }
        public class PostIM {
            [Required(ErrorMessage = "Message is required.")]
            [MaxLength(4096, ErrorMessage = "Message cannot be longer than 4096 characters.")]
            public string Content { get; set; } = default!;
        }
        private class PostCount {
            public Guid? PostId { get; set; }
            public int Count { get; set; }
        }
    }
}
