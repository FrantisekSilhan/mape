using LoginProject.Data;
using LoginProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Areas.Admin.Pages {
    public class IndexModel : PageModel {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context) {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync() {
            User = await _context.Users.ToListAsync();
        }
    }
}
