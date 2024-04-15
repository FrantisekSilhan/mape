using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginProject.Data;
using LoginProject.Models;

namespace LoginProject.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public IndexModel(LoginProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.ParentPost).ToListAsync();
        }
    }
}
