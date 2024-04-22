using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LoginProject.Pages.Posts {
    public class DeleteModel : PageModel {
        private readonly Data.ApplicationDbContext _context;

        public DeleteModel(Data.ApplicationDbContext context) {
            _context = context;
        }
        public async Task<IActionResult> OnPostAsync(Guid? id) {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            if (id == null)
                return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) {
                return NotFound();
            }
            if (!User.IsInRole("admin") && !User.IsInRole("moderator") && post.AuthorId != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
                return Forbid();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            if (post.ParentPostId == null)
                return RedirectToPage("./Index");

            return RedirectToPage("./View", new { id = post.ParentPostId });
        }
    }
}
